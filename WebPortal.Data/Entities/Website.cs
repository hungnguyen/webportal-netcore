using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class Website : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Folder { get; set; }
        public string MobileFolder { get; set; }
        public string DomainAlias { get; set; }
        public string FromEmail { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPServerPort { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPUserPassword { get; set; }
        public bool SMTPSSL { get; set; }
        public string Currency { get; set; }
        public string UploadFolder { get; set; }
        public decimal DeliveryFee { get; set; }
        public int? TotalPageView { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLink { get; set; }
        public bool? IsDown { get; set; }
        public string PageDown { get; set; }
        public bool? IsResetCache { get; set; }
    }
}
