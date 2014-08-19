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

        public EntityResponder<List<Role>> GetRolesByProfileId(int profileId, int langId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var roles = con.Query<Role>("SELECT rpr.ProfileRoleId,rpr.RoleId,r.RoleName,r.RoleTypeId,rpr.TargetId FROM bo.RelationProfileRole AS rpr JOIN [Role] AS r ON r.RoleId = rpr.RoleId WHERE r.LangId = @LangId AND rpr.ProfileId = @ProfileId", new { ProfileId = profileId, LangId = langId });
                if (roles == null || !roles.Any())
                    return new EntityResponder<List<Role>>() { ResponseCode = EnumResponseCode.NoRecordFound, ResponseUserFriendlyMessageKey = Const.Error_InvalidUserNameOrPass };
                return new EntityResponder<List<Role>>() { Data = roles.ToList() };
            }
        }
    }
}
