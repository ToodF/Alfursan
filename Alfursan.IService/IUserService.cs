using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IService
{
    public interface IUserService
    {
        User Login(string email, string pass);

        List<User> GetAll();
        List<User> GetAllByUserType(EnumProfile profile);

        void Set(User user);
    }
}
