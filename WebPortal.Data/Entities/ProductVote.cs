using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class ProductVote : IEntity
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public int? Score { get; set; }
        public int? CusomterID { get; set; }
        public DateTime? DateCreated { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
