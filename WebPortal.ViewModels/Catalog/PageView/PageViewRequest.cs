using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class PageViewRequest
    {
        public int Total { get; set; }
        public DateTime? DateReported { get; set; }
        public int? WebsiteID { get; set; }
    }
}
