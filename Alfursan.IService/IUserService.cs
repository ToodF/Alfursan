using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IService
{
    public interface IUserService
    {
        User Login(string email, string pass);

        List<User> GetAllByUserType(EnumUserType type);
    }
}
