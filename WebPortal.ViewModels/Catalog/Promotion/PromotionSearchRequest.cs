using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class PromotionSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Status? Status { get; set; }
    }
}
