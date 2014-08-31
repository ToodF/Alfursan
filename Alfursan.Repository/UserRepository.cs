﻿using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using Alfursan.Domain;
using Alfursan.IRepository;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Alfursan.Repository
{
    public class UserRepository : IUserRepository
    {
        public EntityResponder<User> Get(int id)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>("select * from [User] where UserId = @UserId", new { UserId = id }).First();
                return new EntityResponder<User>() { Data = user };
            }
        }

        public EntityResponder<List<User>> GetAll()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("SELECT * FROM [User] WHERE IsDeleted = 0 ").ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }

        public Responder Set(User entity)
        {
            entity.Password = Alfursan.Util.Util.EncryptWithMD5(entity.Password);
            using (var con = DapperHelper.CreateConnection())
            {
                var existUser = con.Query<User>("SELECT * FROM [User] WHERE (UserName = @UserName OR Email = @Email) AND IsDeleted = 0", new { entity.UserName, entity.Email });
                if (existUser != null && existUser.Any())
                {
                    return new Responder() { ResponseCode = EnumResponseCode.AlreadyDefined, ResponseUserFriendlyMessageKey = "Error_ExistingUserNameOrEmail" };
                }
                else
                {
                    var result =
                        con.Execute(
                            "insert into [User] (UserName,Email,Password,Name,Surname,CompanyName,Phone,Address,ProfileId) values (@UserName,@Email,@Password,@Name,@Surname,@CompanyName,@Phone,@Address,@ProfileId)",
                            entity);
                    return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NoRecordFound : EnumResponseCode.Successful) };
                }
            }

        }

        public EntityResponder<User> Get(string emailOrUsername)
        {
            throw new System.NotImplementedException();
        }

        public EntityResponder<User> Get(string emailOrUsername, string password)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>("SELECT * FROM [User] WHERE Email = @Email AND [Password] = @Password", new { Email = emailOrUsername, Password = password });
                if (user == null || !user.Any())
                    return new EntityResponder<User>() { ResponseCode = EnumResponseCode.NoRecordFound, ResponseUserFriendlyMessageKey = Const.Error_InvalidUserNameOrPass };
                return new EntityResponder<User>() { Data = user.First() };
            }
        }

        public EntityResponder<List<User>> GetAllByUserType(EnumProfile profile)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("select * from [User] where [ProfileId] = @pofile", new { profile = (int)profile }).ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }

        public Responder ChangePassword(string emailOrUsername, string oldPassword, string newPassword)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute(@"UPDATE [User]
                                            SET Password = @Password
                                        WHERE (Email = @EmailOrUsername OR UserName = @EmailOrUsername)
                                                AND [Password] = @OldPassword 
                                                AND IsDeleted = 0",
                                    new
                                    {
                                        EmailOrUsername = emailOrUsername,
                                        Password = newPassword,
                                        OldPassword = oldPassword
                                    });
            }

            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NoRecordFound : EnumResponseCode.Successful) };
        }

        public Responder Update(User entity)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var existUser = con.Query<User>("SELECT * FROM [User] WHERE " +
                                                "(UserName = @UserName " +
                                                "OR Email = @Email) " +
                                                "AND IsDeleted = 0" +
                                                "AND UserId <> @UserId", entity);
                if (existUser != null && existUser.Any())
                {
                    return new Responder() { ResponseCode = EnumResponseCode.AlreadyDefined, ResponseUserFriendlyMessageKey = "Error_ExistingUserNameOrEmail" };
                }
                else
                {
                    var result = con.Execute(@"update [User] set 
                                            UserName = @UserName
                                            , Email = @Email
                                            , Name = @Name
                                            , Surname = @Surname
                                            , CompanyName = @CompanyName
                                            , Phone = @Phone
                                            , CountryId = @CountryId
                                            , Address = @Address
                                            , ProfileId = @ProfileId
                                            where UserId = @UserId", entity);
                    return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NoRecordFound : EnumResponseCode.Successful) };
                }
            }
        }

        public Responder Delete(int id)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute(@"update [User] set 
                                            IsDeleted = 1,
                                            UpdateDate = Getdate()
                                            where UserId = @UserId", new { UserId = id });
            }
            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotUpdated : EnumResponseCode.Successful) };
        }

        public Responder SaveRelationCustomerCustomOfficer(RelationCustomerCustomOfficer relationCustomerCustomOfficer)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute("INSERT INTO dbo.RelationCustomerCustomOfficer(CustomerUserId,CustomOfficerUserId,CreatedUserId) VALUES (@CustomerUserId,@CustomOfficerUserId,@CreatedUserId)", relationCustomerCustomOfficer);
            }
            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotInserted : EnumResponseCode.Successful) };
        }

        public Responder ChangeStatus(int userId, bool status)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var result = con.Execute(@"update [User] set 
                                            Status = @Status
                                            where UserId = @UserId", new { UserId = userId, Status = status });
                return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NoRecordFound : EnumResponseCode.Successful) };
            }
        }

        public EntityResponder<User> GetCustomerUserId(int customOfficerId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>(@"SELECT u.*
                                            FROM   RelationCustomerCustomOfficer rcco
                                                   INNER JOIN [User] u
                                                           ON u.UserId = rcco.CustomerUserId
                                            WHERE  CustomOfficerId = @CustomOfficerId",
                                           new { CustomOfficerId = customOfficerId }).FirstOrDefault();

                return new EntityResponder<User>() { Data = user };
            }
        }

        public EntityResponder<List<User>> GetCustomers()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("SELECT * FROM [User] WHERE ProfileId = 3").ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }


        public EntityResponder<User> GetUserByEmail(string email)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>("select * from [User] where Email = @Email", new { Email = email }).First();
                return new EntityResponder<User>() { Data = user };
            }
        }
    }
}
