using System;
using System.Collections.Generic;
using System.Linq;
using Alfursan.Domain;
using Alfursan.IRepository;
using Dapper;

namespace Alfursan.Repository
{
    public class AlfursanFileRespository : IAlfursanFileRepository
    {
        public EntityResponder<AlfursanFile> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Responder Set(AlfursanFile file)
        {
            var result = 0;
            using (var con = DapperHelper.CreateConnection())
            {
                result = con.Execute("INSERT INTO dbo.[File](CustomerUserId,OriginalFileName,RelatedFileName,Subject,Description,CreatedUserId,CreateDate,UpdateDate,IsDeleted,FileType) VALUES (@CustomerUserId,@OriginalFileName,@RelatedFileName,@Subject,@Description,@CreatedUserId,@CreateDate,@UpdateDate,@IsDeleted,@FileType)", file);
            }
            return new Responder() { ResponseCode = (result == 0 ? EnumResponseCode.NotInserted : EnumResponseCode.Successful) };
        }

        public EntityResponder<List<AlfursanFile>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Responder Update(AlfursanFile entity)
        {
            throw new NotImplementedException();
        }

        public Responder Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AlfursanFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public List<AlfursanFile> GetFilesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<AlfursanFile> GetLastFilesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<AlfursanFile> GetFilesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<AlfursanFile> SearchFilesByUserIdKeyword(int userId, string keywords)
        {
            throw new NotImplementedException();
        }

        public EntityResponder<List<AlfursanFile>> GetFiles(int userId, int customerUserId)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var files = con.Query<AlfursanFile, User, User, AlfursanFile>(@"SELECT f.Subject
	                                                                                ,f.OriginalFileName
	                                                                                ,f.FileType
	                                                                                ,RelatedFileName
	                                                                                ,f.CreateDate
	                                                                                ,customer.UserId AS CustomerUserId
	                                                                                ,customer.Name
	                                                                                ,customer.Surname
	                                                                                ,createdUser.UserId AS CreatedUserId
	                                                                                ,createdUser.Name
	                                                                                ,createdUser.Surname
                                                                                FROM [File] f
                                                                                INNER JOIN [user] currentUser ON currentUser.UserId = @CurrentUserId
                                                                                LEFT OUTER JOIN RelationCustomerCustomOfficer rcco ON f.CustomerUserId = rcco.CustomerUserId
                                                                                LEFT OUTER JOIN [User] createdUser ON f.CreatedUserId = createdUser.UserId
                                                                                LEFT OUTER JOIN [User] customer ON f.CustomerUserId = customer.UserId
                                                                                WHERE f.IsDeleted = 0
	                                                                                AND currentUser.IsDeleted = 0
	                                                                                AND currentUser.ProfileId IN (1,2)
	                                                                                OR (
		                                                                                currentUser.ProfileId = 3
		                                                                                AND rcco.CustomerUserId = @CustomerUserId
		                                                                                )
	                                                                                OR (
		                                                                                FileType = 1
		                                                                                AND rcco.CustomerUserId = @CustomerUserId
		                                                                                )
                                                                                ORDER BY CreateDate DESC"
                                                              , (file, customer, createdUser) =>
                                                              {
                                                                  file.Customer = customer;
                                                                  file.CreatedUser = createdUser;
                                                                  return file;
                                                              },
                                           new { CurrentUserId = userId, CustomerUserId = customerUserId }, splitOn: "CustomerUserId,CreatedUserId").ToList();

                return new EntityResponder<List<AlfursanFile>>() { Data = files };
            }
        }
    }
}
