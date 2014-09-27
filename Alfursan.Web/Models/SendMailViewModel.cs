using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alfursan.Domain;

namespace Alfursan.Web.Models
{
    public class SendMailViewModel
    {
        public List<AlfursanFile> Files { get; set; }

        public List<User> Users { get; set; }
    }
}