
using Alfursan.Domain;
using System.Collections.Generic;

namespace Alfursan.IRepository
{
    public interface IRoleRepository : IRepository<Role>
    {
        EntityResponder<List<Role>> GetRolesByProfileId(int profileId,int langId);
    }
}
