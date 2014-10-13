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

        public void Log(System.Exception ex, string method)
        {
            using (var con = DapperHelper.CreateConnection())
            {
                con.Execute(
                    "INSERT INTO [dbo].[Log] ([MethodInfo] ,[ExceptionType] ,[Message] ,[InnerException] ,[StackTrace] ,[MessageDate]) VALUES (@MethodInfo ,@ExceptionType ,@Message ,@InnerException ,@StackTrace ,GetDate())",
                    new { Message = ex.Message, InnerException = (ex.InnerException != null ? ex.InnerException.Message : ""), StackTrace = ex.StackTrace, ExceptionType = ex.GetType().FullName, MethodInfo = method });
            }
        }
    }
}
