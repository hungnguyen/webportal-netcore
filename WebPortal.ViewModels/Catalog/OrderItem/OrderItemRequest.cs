using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class OrderItemRequest
    {
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
    }
}
