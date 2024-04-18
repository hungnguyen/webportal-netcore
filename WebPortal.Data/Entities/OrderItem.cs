using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class OrderItem : IEntity
    {
        public int ID { get; set; }
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
