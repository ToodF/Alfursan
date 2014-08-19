using Alfursan.Domain;
using System.Collections.Generic;

namespace Alfursan.IService
{
    public interface IRoleService
    {
        EntityResponder<List<Role>> GetRolesByProfileId(int profileId, int langId);
    }
}
