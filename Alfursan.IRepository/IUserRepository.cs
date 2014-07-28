using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface IUserRepository :IRepository<User>
    {
        User Get(string emailOrUsername);
        User Get(string emailOrUsername,string password);
        List<User> GetAllByUserType(EnumProfile profile);
        void ChangePassword(string emailOrUsername,string oldPassword,string newPassword);
    }
}
