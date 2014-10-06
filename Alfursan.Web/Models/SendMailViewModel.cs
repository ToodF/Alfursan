using System.Web.Mvc;
using Alfursan.Domain;
using System.Collections.Generic;

namespace Alfursan.Web.Models
{
    public class SendMailViewModel
    {
        public List<AlfursanFile> Files { get; set; }

        public List<User> Users { get; set; }
        public string Subject { get; set; }
        public string Emails { get; set; }
        [AllowHtml]
        public string MailBody { get; set; }
    }
}