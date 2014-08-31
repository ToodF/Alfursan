using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfursan.Domain;

namespace Alfursan.IService
{
    public interface IAlfursanFileService
    {
        Responder Set(AlfursanFile file);
        EntityResponder<List<AlfursanFile>> GetFiles(int userId, int customerUserId);
    }
}
