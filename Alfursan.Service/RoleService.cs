using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IRepository;
using Alfursan.IService;
using System.Collections.Generic;

namespace Alfursan.Service
{
    public class RoleService : IRoleService
    {
        private IRoleRepository rolerRepository;

        public RoleService()
        {
            rolerRepository = IocContainer.Resolve<IRoleRepository>();
        }
        public EntityResponder<List<Role>> GetRolesByProfileId(int profileId, int langId)
        {
            var roles = rolerRepository.GetRolesByProfileId(profileId, langId);
            return roles;
        }
    }
}
