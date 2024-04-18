using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class TransactionSearchRequest : SearchRequest
    {
        public int? OrderID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
