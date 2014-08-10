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
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("select * from [User] ").ToList();
                return users;
            }
        }

        public void Set(User entity)
        {
            using (var con = DapperHelper.CreateConnection())
            {
              //var result =   con.Execute("insert into [User] (UserName,Email,Password,Name,Surname,CompanyName,Phone,Address,ProfileId) values ('@UserName','@Email','@Password','@Name','@Surname','@CompanyName','@Phone','@Address',@ProfileId)",new
              //  {
              //      UserName = entity.UserName
              //      , Email = entity.Email
              //      , Password = entity.Password
              //      , Name = entity.Name
              //      , Surname =  entity.Surname
              //      , CompanyName =  entity.CompanyName
              //      , Phone = entity.Phone
              //      , Address = entity.Address
              //      , ProfileId = entity.ProfileId
              //  });

                 var result =   con.Execute("insert into [User] (UserName,Email,Password,Name,Surname,CompanyName,Phone,Address,ProfileId) values (@UserName,@Email,@Password,@Name,@Surname,@CompanyName,@Phone,@Address,@ProfileId)",entity);
            }
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
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("select * from [User] where [ProfileId] = @pofile", new { profile = (int)profile}).ToList();
                return users;
            }
        }


        public void ChangePassword(string emailOrUsername, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}
