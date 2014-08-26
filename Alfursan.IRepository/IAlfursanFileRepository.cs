using Alfursan.Domain;
using System;
using System.Collections.Generic;

namespace Alfursan.IRepository
{
    public interface IAlfursanFileRepository : IRepository<AlfursanFile>
    {
        List<AlfursanFile> GetFiles();

        List<AlfursanFile> GetFilesByUserId(int userId);

        List<AlfursanFile> GetLastFilesByUserId(int userId);

        List<AlfursanFile> GetFilesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate);

        List<AlfursanFile> SearchFilesByUserIdKeyword(int userId, string keywords);
    }
}
