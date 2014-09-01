using Alfursan.Domain;
using System.Collections.Generic;

namespace Alfursan.IService
{
    public interface IRoleService
    {
        EntityResponder<List<Role>> GetRolesByProfileId(int profileId);

        EntityResponder<List<Role>> GetAll();

        Responder SetRoles(List<Role> roles);
        Responder DeleteRolesByProfileId(int profileId);
    }
}
