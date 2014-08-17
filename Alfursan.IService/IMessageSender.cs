
using System.Net.Mail;
using Alfursan.Domain;

namespace Alfursan.IService
{
   public interface IMessageSender
   {
       Responder SendMessage(MailMessage message);

   }
}
