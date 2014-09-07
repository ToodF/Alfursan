using Alfursan.Domain;
using Alfursan.IService;
using System.Net;
using System.Net.Mail;

namespace Alfursan.Service
{
    public class MailService : IMessageSender
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public MailService(string host, int port, string userName, string password)
        {
            Host = host;
            Port = port;
            UserName = userName;
            Password = password;
        }
        public Responder SendMessage(MailMessage mailMessage)
        {
            var mailProvider = new MailProvider();
            mailProvider.Host = Host;
            mailProvider.Port = Port;//587;//465
            mailProvider.UserName = UserName;
            mailProvider.Password = Password;
            //TODO:Provider bilgileri set edilecek
            mailMessage.IsBodyHtml = true;
            var smtp = new SmtpClient(mailProvider.Host)
            {
                Port = mailProvider.Port,
                Credentials = new NetworkCredential(mailProvider.UserName, mailProvider.Password)
            };
            smtp.Send(mailMessage);
            return new Responder();
        }
    }
}
