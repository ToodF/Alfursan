using System.Runtime.Remoting.Messaging;
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
                var userResult = con.Query<User, RelationCustomerCustomOfficer, User>(@"select 
	                    [UserId]
                            ,[UserName]
                            ,[Email]
                            ,[Password]
                            ,[Name]
                            ,[Surname]
                            ,[CompanyName]
                            ,[Phone]
                            ,[CountryId]
                            ,[Address]
                            ,[ProfileId]
                            ,[RelationId]
                            ,[CustomerUserId]
                            ,[CustomOfficerId]
                        from [User] as u
                    left join RelationCustomerCustomOfficer as rcco on u.UserId = rcco.CustomerUserId and rcco.IsDeleted = 0
                    where UserId = @UserId "
                    , (user, relation) =>
                    {
                        user.RelationCustomerCustomOfficer = relation;
                        return user;
                    }
                    , new { UserId = id }, splitOn: "RelationId");
                return new EntityResponder<User>() { Data = userResult.First() };
            }
        }

        public EntityResponder<List<User>> GetAll()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("SELECT U.*,CustomOfficer.Name + ' ' + CustomOfficer.Surname AS CustomOfficer FROM [User] AS U LEFT JOIN dbo.RelationCustomerCustomOfficer AS Customer ON U.UserId = Customer.CustomerUserId AND Customer.IsDeleted = 0 LEFT JOIN [User] AS CustomOfficer ON CustomOfficer.UserID = Customer.CustomOfficerId  WHERE U.IsDeleted = 0").ToList();
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
                            "insert into [User] (UserName,Email,Password,Name,Surname,CompanyName,Phone,CountryId,Address,ProfileId,IsDeleted) values (@UserName,@Email,@Password,@Name,@Surname,@CompanyName,@Phone,@CountryId,@Address,@ProfileId,0)",
                            entity);
                    return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotInserted : EnumResponseCode.Successful) };
                }
            }

        }

        public EntityResponder<User> Get(string emailOrUsername, string password)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>("SELECT * FROM [User] WHERE IsDeleted = 0 and Status = 1 and Email = @Email AND [Password] = @Password", new { Email = emailOrUsername, Password = password });
                if (user == null || !user.Any())
                    return new EntityResponder<User>() { ResponseCode = EnumResponseCode.NoRecordFound, ResponseUserFriendlyMessageKey = Const.Error_InvalidUserNameOrPass };
                return new EntityResponder<User>() { Data = user.First() };
            }
        }

        public EntityResponder<List<User>> GetAllByUserType(EnumProfile profile)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("select * from [User] where IsDeleted = 0 and [ProfileId] = @profile", new { profile = (int)profile }).ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }

        public Responder ChangePassword(string emailOrUsername, string newPassword)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute(@"UPDATE [User]
                                            SET Password = @Password
                                        WHERE (Email = @EmailOrUsername OR UserName = @EmailOrUsername)                                                AND IsDeleted = 0",
                                    new
                                    {
                                        EmailOrUsername = emailOrUsername,
                                        Password = Alfursan.Util.Util.EncryptWithMD5(newPassword)
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
                    var result = con.Execute(@"if(@ProfileId <> 3)
                                                begin 
	                                                UPDATE RelationCustomerCustomOfficer SET IsDeleted = 1 , UpdateDate = Getdate() WHERE CustomerUserId = @UserId
                                                end              
                                                update [User] set 
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
                result = con.Execute(@"IF (NOT EXISTS (SELECT * FROM RelationCustomerCustomOfficer WHERE IsDeleted = 0 AND CustomerUserId = @CustomerUserId AND CustomOfficerId = @CustomOfficerId))
                    BEGIN
	                    UPDATE RelationCustomerCustomOfficer SET IsDeleted = 1 , UpdateDate = Getdate() WHERE CustomerUserId = @CustomerUserId
	                    INSERT INTO dbo.RelationCustomerCustomOfficer(CustomerUserId,CustomOfficerId,CreatedUserId,IsDeleted) VALUES (@CustomerUserId,@CustomOfficerId,@CreatedUserId,0)
                    END"
                    , relationCustomerCustomOfficer);
            }
            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotInserted : EnumResponseCode.Successful) };
        }

        public Responder DeleteRelationCustomerCustomOfficer(int customerUserId)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute(@"
	                    UPDATE RelationCustomerCustomOfficer SET IsDeleted = 1 , UpdateDate = Getdate() WHERE CustomerUserId = @CustomerUserId
	                  ", new { CustomerUserId = customerUserId });
            };
            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotUpdated : EnumResponseCode.Successful) };
        }

        public EntityResponder<List<User>> GetUsersForNotificationByCustomerUserId(int customerUserId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>(@"SELECT DISTINCT u.Email,
                                                            u.Name,
                                                            u.Surname
                                            FROM   [dbo].[RelationCustomerCustomOfficer] rcco
                                                   INNER JOIN [dbo].[user] u
                                                           ON u.UserId = rcco.CustomOfficerId
                                                               OR u.UserId = rcco.CustomerUserId
                                            WHERE  rcco.CustomerUserId = @CustomerUserId",
                                           new { CustomerUserId = customerUserId }).ToList();

                return new EntityResponder<List<User>>() { Data = user };
            }
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

        public EntityResponder<List<User>> GetCustomersByCustomOfficerId(int customOfficerId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>(@"SELECT u.*
                                            FROM   RelationCustomerCustomOfficer rcco
                                                   INNER JOIN [User] u
                                                           ON u.UserId = rcco.CustomerUserId
                                            WHERE  CustomOfficerId = @CustomOfficerId",
                    new { CustomOfficerId = customOfficerId });
                if (user == null || !user.Any())
                {
                    return new EntityResponder<List<User>>() { ResponseCode = EnumResponseCode.NoRecordFound };
                }
                return new EntityResponder<List<User>>() { Data = user.ToList() };
            }
        }

        public EntityResponder<List<User>> GetCustomers()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("SELECT * FROM [User] WHERE ProfileId = 3 AND IsDeleted = 0").ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }

        public EntityResponder<User> GetActiveUserByEmail(string email)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var user = con.Query<User>(
                    "select * from [User] where IsDeleted = 0 and Status = 1 and Email = @Email", new { Email = email });
                if (user == null || !user.Any())
                {
                    return new EntityResponder<User>() { ResponseCode = EnumResponseCode.NoRecordFound, ResponseUserFriendlyMessageKey = "Error_EmailNotFound" };
                }
                return new EntityResponder<User>() { Data = user.First() };
            }
        }

        public EntityResponder<List<Country>> GetCountries()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var countries = con.Query<Country>("SELECT CountryId,CountryName FROM Country WHERE LangId = 1").ToList();
                return new EntityResponder<List<Country>>() { Data = countries };
            }
        }

        public EntityResponder<List<User>> GetCustomOfficers()
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var users = con.Query<User>("select * from [User] u where IsDeleted = 0 and [ProfileId] = @profile", new { profile = (int)EnumProfile.CustomOfficer }).ToList();
                return new EntityResponder<List<User>>() { Data = users };
            }
        }

        public Responder SetConfirmKey(string email, string confirmKey)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var result = con.Execute(@"update [User] set 
                                            ConfirmKey = @ConfirmKey
                                            where Email = @Email", new { ConfirmKey = confirmKey, Email = email });
                return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NoRecordFound : EnumResponseCode.Successful) };
            }
        }
    }
}
