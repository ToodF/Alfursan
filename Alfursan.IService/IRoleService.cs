using Alfursan.Domain;
using System.Collections.Generic;

namespace Alfursan.IService
{
    public interface IRoleService
    {
        EntityResponder<List<Role>> GetRolesByProfileId(int profileId);
        Responder SetRoles(List<Role> roles);

        Responder DeleteRolesByProfileId(int profileId);
    }
}
