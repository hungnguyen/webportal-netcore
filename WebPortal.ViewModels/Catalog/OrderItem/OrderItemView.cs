using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class OrderItemView
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductLink { get; set; }
        public string ProductImage { get; set; }
        public decimal Amount { get; set; }
    }
}
