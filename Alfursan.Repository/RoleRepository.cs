using System.Linq;
using Alfursan.Domain;
using Alfursan.IRepository;
using System;
using System.Collections.Generic;
using Dapper;

namespace Alfursan.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public EntityResponder<Role> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Responder Set(Role entity)
        {
            throw new NotImplementedException();
        }

        public EntityResponder<List<Role>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Responder Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public Responder Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EntityResponder<List<Role>> GetRolesByProfileId(int profileId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var roles = con.Query<Role>("SELECT rpr.ProfileRoleId,rpr.RoleId FROM dbo.RelationProfileRole AS rpr WHERE rpr.ProfileId = @ProfileId", new { ProfileId = profileId });
                if (roles == null || !roles.Any())
                    return new EntityResponder<List<Role>>() { ResponseCode = EnumResponseCode.NoRecordFound, ResponseUserFriendlyMessageKey = Const.Error_InvalidUserNameOrPass };
                return new EntityResponder<List<Role>>() { Data = roles.ToList() };
            }
        }

        public Responder SetRoles(List<Role> roles)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                foreach (var role in roles)
                {
                    con.Execute("Insert into dbo.RelationProfileRole (RoleId,ProfileId) Values (@RoleId,@ProfileId)", role);
                }

                return new EntityResponder<List<Role>>() { ResponseCode = EnumResponseCode.Successful };
            }
        }

        public Responder DeleteRolesByProfileId(int profileId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                con.Execute("Update dbo.RelationProfileRole set IsDeleted = 1 , DeletedDate = GETDATE() where ProfileId = @ProfileId", new { ProfileId = profileId });
                return new EntityResponder<List<Role>>() { ResponseCode = EnumResponseCode.Successful };
            }
        }
    }
}
