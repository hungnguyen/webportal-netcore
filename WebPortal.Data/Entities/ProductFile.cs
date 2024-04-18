using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class ProductFile : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string FileName { get; set; }
        public string Link { get; set; }
        public int? OrderNumber { get; set; }
        public int? ProductID { get; set; }
        public Status Status { get; set; }
        public Product Product { get; set; }
    }
}
