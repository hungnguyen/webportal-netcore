using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class ProductCommentSearchRequest : SearchRequest
    {
        public int? ProductID { get; set; }
        public Status? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
