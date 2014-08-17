using Alfursan.Domain;
using Alfursan.IService;
using System.Net;
using System.Net.Mail;

namespace Alfursan.Service
{
    public class SendMailService : IMessageSender
    {
        public Responder SendMessage(MailMessage mailMessage)
        {
            var isSend = false;
            var mailProvider = new MailProvider();
            mailProvider.Host = "smtp.gmail.com";
            mailProvider.Port = 587;//465
            mailProvider.UserName = "";
            mailProvider.Password = "";
            //TODO:Provider bilgileri set edilecek
            mailMessage.IsBodyHtml = true;
            var smtp = new SmtpClient(mailProvider.Host)
            {
                Port = mailProvider.Port,
                Credentials = new NetworkCredential(mailProvider.UserName, mailProvider.Password)
            };
            smtp.Send(mailMessage);
            isSend = true;
            return new Responder();
        }
    }
}
