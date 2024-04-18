using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class PageViewSearchRequest : SearchRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
