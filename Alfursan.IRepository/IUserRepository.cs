using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface IUserRepository :IRepository<User>
    {
        User Get(string userName);
        User Get(string email,string password);
        List<User> GetAllByUserType(EnumUserType userType);
    }
}
