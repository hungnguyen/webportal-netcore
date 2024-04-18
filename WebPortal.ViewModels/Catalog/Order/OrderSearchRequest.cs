using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class OrderSearchRequest : SearchRequest
    {
        public int? OrderID { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public PayStatus? PayStatus { get; set; }
        public int CustomerID { get; set; }
        public Guid SaleID { get; set; }    
    }
}
