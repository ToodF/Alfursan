using Alfursan.Domain;
using Alfursan.IService;
using System.Net;
using System.Net.Mail;

namespace Alfursan.Service
{
    public class MailService : IMessageSender
    {
        private MailProvider mailProvider;
        public MailService(MailProvider mailProvider)
        {
            this.mailProvider = mailProvider;
        }
        public Responder SendMessage(MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress(mailProvider.UserName);
            mailMessage.IsBodyHtml = true;
            var smtp = new SmtpClient()
            {
                Host = mailProvider.Host,
                Port = mailProvider.Port,
                Credentials = new NetworkCredential(mailProvider.UserName, mailProvider.Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtp.Send(mailMessage);
            return new Responder();
        }
    }
}
