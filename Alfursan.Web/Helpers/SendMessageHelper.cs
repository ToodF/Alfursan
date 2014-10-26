using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

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
            message.Body += Resources.MailMessage.Signature;
            var replacements = new Dictionary<string, string>();
            replacements.Add("<Name>", user.Name);
            replacements.Add("<Surname>", user.Surname);
            replacements.Add("<Username>", user.UserName);
            replacements.Add("<Email>", user.Email);
            replacements.Add("<Password>", user.Password);
            message.To.Add(user.Email);
            return mailsender.SendMessage(message, replacements);
        }

        public static Responder SendMessageForgot(User user, string confirmKey)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = Resources.MailMessage.ForgotPasswordSubject;
            message.Body = Resources.MailMessage.ForgotPasswordBody;
            message.Body += Resources.MailMessage.Signature;
            var replacements = new Dictionary<string, string>();
            replacements.Add("<Name>", user.Name);
            replacements.Add("<Surname>", user.Surname);
            replacements.Add("<ConfirmKey>", confirmKey);
            replacements.Add("<SiteRoot>", ConfigurationManager.AppSettings["SiteRoot"]);

            message.To.Add(user.Email);
            return mailsender.SendMessage(message, replacements);
        }

        public static Responder SendMessageFileUploaded(List<string> emails, List<string> absolutePaths, string body, string subject)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = subject;

            message.Body = body;
            message.Body += Resources.MailMessage.Signature;

            foreach (var absolutePath in absolutePaths)
            {
                var attachment = new Attachment(absolutePath);
                message.Attachments.Add(attachment);
            }
            foreach (var email in emails)
            {
                message.To.Add(email);
            }
            return mailsender.SendMessage(message, null);
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
                message.Body += Resources.MailMessage.Signature;
                var replacements = new Dictionary<string, string>();
                replacements.Add("<Name>", user.Name);
                replacements.Add("<Surname>", user.Surname);
                replacements.Add("<Username>", user.UserName);
                replacements.Add("<Email>", user.Email);
                replacements.Add("<Password>", user.Password);
                message.To.Add(user.Email);
                return mailsender.SendMessage(message, replacements);
            }
            return userResponse;
        }

        public static Responder SendMessage(string email, string body, string subject)
        {
            var mailsender = IocContainer.Resolve<IMessageSender>();
            var message = new MailMessage();
            message.Subject = subject;

            message.Body = body;
            message.Body += Resources.MailMessage.Signature;
            message.To.Add(email);
            return mailsender.SendMessage(message, null);
        }
    }
}