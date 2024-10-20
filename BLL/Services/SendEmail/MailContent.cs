using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.SendEmail
{
    public class MailContent
    {
        public string To { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
