using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class MailBox : IEntity
    {
        public int ID { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int? OrderID { get; set; }
        public string LanguageID { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? WebsiteID { get; set; }
    }
}
