using System.Collections.Generic;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IRepository;
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
        public Responder SendMessage(MailMessage mailMessage, Dictionary<string, string> replacements)
        {
            if (replacements != null)
            {
                foreach (var replacement in replacements)
                {
                    mailMessage.Body = mailMessage.Body.Replace(replacement.Key, replacement.Value);
                    mailMessage.Subject = mailMessage.Subject.Replace(replacement.Key, replacement.Value);
                }
            }
            mailMessage.From = new MailAddress(mailProvider.UserName);
            mailMessage.IsBodyHtml = true;
            var smtp = new SmtpClient()
            {
                Host = mailProvider.Host,
                Port = mailProvider.Port,
                Credentials = new NetworkCredential(mailProvider.UserName, mailProvider.Password),
                EnableSsl = mailProvider.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtp.Send(mailMessage);
            return Log(mailMessage);
        }

        public Responder Log(MailMessage mailMessage)
        {
            var mailLog = new MailLog();
            mailLog.MailFrom = mailMessage.From.Address;
            foreach (var to in mailMessage.To)
            {
                mailLog.MailTo += to.Address + ";";
            }
            foreach (var cc in mailMessage.CC)
            {
                mailLog.MailCc += cc.Address + ";";
            }
            foreach (var bcc in mailMessage.Bcc)
            {
                mailLog.MailBcc += bcc.Address + ";";
            }
            mailLog.Subject = mailMessage.Subject;
            mailLog.MailBody = mailMessage.Body;
            foreach (var attachment in mailMessage.Attachments)
            {
                mailLog.Attachments += attachment.Name + ";";
            }

            var logger = IocContainer.Resolve<ILoggerRepository>();
            logger.Log(mailLog);
            return new Responder();
        }
    }
}
