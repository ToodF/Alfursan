using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Alfursan.Domain;
using Alfursan.IRepository;
using Dapper;

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

        public User Get(string userName)
        {
            throw new System.NotImplementedException();
        }

        public User Get(string email, string password)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                con.Open();
                var user = con.Query<User>("SELECT * FROM [User] WHERE Email = @Email AND [Password] = @pass", new { Email = email, Password = password }).First();
                con.Close();
                return user;
            }
        }


        public List<User> GetAllByUserType(EnumUserType userType)
        {
            throw new System.NotImplementedException();
        }
    }
}
