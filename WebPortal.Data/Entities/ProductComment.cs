using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class ProductComment : IEntity
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }
        public int? LikeCount { get; set; }
        public string PeopleLike { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CustomerID { get; set; }
        public int? Quality { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
