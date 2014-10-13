using System;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface ILoggerRepository
    {
        Responder Log(MailLog mailLog);

        void Log(Exception ex, string method);
    }
}
