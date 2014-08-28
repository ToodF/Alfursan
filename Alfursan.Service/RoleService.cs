using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IRepository;
using Alfursan.IService;
using System.Collections.Generic;

namespace Alfursan.Service
{
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository;

        public RoleService()
        {
            roleRepository = IocContainer.Resolve<IRoleRepository>();
        }
        public EntityResponder<List<Role>> GetRolesByProfileId(int profileId)
        {
            var roles = roleRepository.GetRolesByProfileId(profileId);
            return roles;
        }

        public Responder SetRoles(List<Role> roles)
        {
            return roleRepository.SetRoles(roles);
        }

        public Responder DeleteRolesByProfileId(int profileId)
        {
            return roleRepository.DeleteRolesByProfileId(profileId);
        }
    }
}
