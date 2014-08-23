﻿using System.Runtime.Remoting.Messaging;
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
                var result = con.Execute("insert into [User] (UserName,Email,Password,Name,Surname,CompanyName,Phone,Address,ProfileId) values (@UserName,@Email,@Password,@Name,@Surname,@CompanyName,@Phone,@Address,@ProfileId)", entity);
            }
            return new Responder();
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
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute(@"update [User] set 
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
            }

            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotUpdated : EnumResponseCode.Successful) };
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
            return new Responder(){ResponseCode = (result == 0 ? EnumResponseCode.NotUpdated : EnumResponseCode.Successful)};
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
    }
}
