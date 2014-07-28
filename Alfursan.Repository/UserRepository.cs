using Alfursan.Domain;
using Alfursan.IRepository;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Alfursan.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Set(User entity)
        {
            throw new System.NotImplementedException();
        }

        public User Get(string emailOrUsername)
        {
            throw new System.NotImplementedException();
        }

        public User Get(string emailOrUsername, string password)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>("SELECT * FROM [User] WHERE Email = @Email AND [Password] = @Password", new { Email = emailOrUsername, Password = password }).First();
                return user;
            }
        }


        public List<User> GetAllByUserType(EnumProfile profile)
        {
            throw new System.NotImplementedException();
        }


        public void ChangePassword(string emailOrUsername, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}
