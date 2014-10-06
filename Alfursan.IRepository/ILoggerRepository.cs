using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface ILoggerRepository
    {
        Responder Log(MailLog mailLog);
    }
}
