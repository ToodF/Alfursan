using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using System.Net.Mail;
using Alfursan.Web.Models;

namespace Alfursan.Web.Helpers
{
    public class SendMessageHelper
    {
        public static Responder SendMessageNewUser(UserViewModel user)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = Resources.MailMessage.WelcomeSubject;
            switch (user.ProfileId)
            {
                case EnumProfile.Admin:
                    message.Body = Resources.MailMessage.WelcomeAdminBody;
                    break;
                case EnumProfile.User:
                    message.Body = Resources.MailMessage.WelcomeUserBody;
                    break;
                case EnumProfile.Customer:
                    message.Body = Resources.MailMessage.WelcomeCustomerBody;
                    break;
                case EnumProfile.CustomOfficer:
                    message.Body = Resources.MailMessage.WelcomeCustomOfficerBody;
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
            return mailsender.SendMessage(message);
        }

        public static Responder SendMessageForgot(User user, string confirmKey)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = Resources.MailMessage.ForgotPasswordSubject;
            message.Body = Resources.MailMessage.ForgotPasswordBody;
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
            return mailsender.SendMessage(message);
        }

        public static Responder SendMessageFileUploaded(User user, List<string> absolutePaths, string body, string subject)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = subject;

            message.Body = body;

            foreach (var absolutePath in absolutePaths)
            {
                var attachment = new Attachment(absolutePath);
                message.Attachments.Add(attachment);
            }

            var replacements = new Dictionary<string, string>();
            replacements.Add("<Name>", user.Name);
            replacements.Add("<Surname>", user.Surname);
            replacements.Add("<SiteRoot>", ConfigurationManager.AppSettings["SiteRoot"]);

            foreach (var replacement in replacements)
            {
                message.Body = message.Body.Replace(replacement.Key, replacement.Value);
            }
            message.To.Add(user.Email);
            return mailsender.SendMessage(message);
        }
        
        internal static Responder SendMessageChangePass(ChangePassViewModel changePassViewModel)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var userResponse = userService.GetActiveUserByEmail(changePassViewModel.Email);
            if (userResponse.ResponseCode == EnumResponseCode.Successful)
            {
                var user = userResponse.Data;
                var mailsender = IocContainer.Resolve<IMessageSender>();
                var message = new MailMessage();
                message.Subject = Resources.MailMessage.ChangePassSubject;
                message.Body = Resources.MailMessage.ChangePassBody;
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
                return mailsender.SendMessage(message);
            }
            return userResponse;
        }
    }
}