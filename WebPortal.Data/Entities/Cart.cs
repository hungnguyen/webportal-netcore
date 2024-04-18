using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class Cart : IEntity
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public Guid SessionID { get; set; }
        public int? CustomerID { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
