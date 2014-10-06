using Alfursan.Domain;
using Alfursan.IRepository;
using Dapper;

namespace Alfursan.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        public Responder Log(MailLog mailLog)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                var result =
                    con.Execute(
                        "INSERT INTO [MailLog]([MailFrom],[MailTo],[MailCc],[MailBcc],[Subject],[MailBody],[Attachments])VALUES(@MailFrom,@MailTo,@MailCc,@MailBcc,@Subject,@MailBody,@Attachments)",
                        mailLog);
                return new Responder()
                {
                    ResponseCode = (result == 0 ? EnumResponseCode.NotInserted : EnumResponseCode.Successful)
                };
            }
        }
    }
}
