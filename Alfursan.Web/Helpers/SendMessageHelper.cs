using System.Collections.Generic;
using System.Configuration;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using System.Net.Mail;
using Alfursan.Web.Models;

namespace Alfursan.Web.Helpers
{
    public class SendMessageHelper
    {
        public static void SendMessageNewUser(UserViewModel user)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = Alfursan.Resx.MailMessage.WelcomeSubject;
            switch (user.ProfileId)
            {
                case EnumProfile.Admin:
                    message.Body = Resx.MailMessage.WelcomeAdminBody;
                    break;
                case EnumProfile.User:
                    message.Body = Resx.MailMessage.WelcomeUserBody;
                    break;
                case EnumProfile.Customer:
                    message.Body = Resx.MailMessage.WelcomeCustomerBody;
                    break;
                case EnumProfile.CustomOfficer:
                    message.Body = Resx.MailMessage.WelcomeCustomOfficerBody;
                    break;
                default:
                    break;
            }
            var replacements = new Dictionary<string, string>();
            replacements.Add("<Name>", user.Name);
            replacements.Add("<Surname>", user.Surname);
            replacements.Add("<Username>", user.UserName);
            replacements.Add("<Email>", user.Email);
            replacements.Add("<Password>", user.Password);
            foreach (var replacement in replacements)
            {
                message.Body = message.Body.Replace(replacement.Key, replacement.Value);
            }
            message.To.Add(user.Email);
            mailsender.SendMessage(message);
        }

        public static void SendMessageForgot(User user, string confirmKey)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = Resx.MailMessage.ForgotPasswordSubject;
            message.Body = Resx.MailMessage.ForgotPasswordBody;
            var replacements = new Dictionary<string, string>();
            replacements.Add("<Name>", user.Name);
            replacements.Add("<Surname>", user.Surname);
            replacements.Add("<ConfirmKey>", confirmKey);
            replacements.Add("<SiteRoot>", ConfigurationManager.AppSettings["SiteRoot"]);
            foreach (var replacement in replacements)
            {
                message.Body = message.Body.Replace(replacement.Key, replacement.Value);
            }
            message.To.Add(user.Email);
            mailsender.SendMessage(message);
        }

    }
}