using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class WebsiteRequest
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Folder { get; set; }
        [Display(Name = "Mobile Folder")]
        public string MobileFolder { get; set; }
        [Display(Name = "Domain Alias")]
        public string DomainAlias { get; set; }
        [Display(Name = "From Email")]
        public string FromEmail { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPServerPort { get; set; }
        public string SMTPUserName { get; set; }
        [DataType(DataType.Password)]
        public string SMTPUserPassword { get; set; }
        public bool SMTPSSL { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Upload Folder")]
        public string UploadFolder { get; set; }
        [Display(Name = "Delivery Fee")]
        public decimal DeliveryFee { get; set; }
        public int TotalPageView { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Link")]
        public string ProjectLink { get; set; }
        [Display(Name = "Website is down")]
        public bool IsDown { get; set; }
        [Display(Name = "Page Down Url")]
        public string PageDown { get; set; }
        public bool IsResetCache { get; set; }
    }
}
