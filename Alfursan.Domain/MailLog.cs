
namespace Alfursan.Domain
{
    public class MailLog
    {
        public int MailLogId { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string Attachments { get; set; }
    }
}
