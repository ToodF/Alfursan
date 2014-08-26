using System;
using System.Collections.Generic;
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
    }
}
