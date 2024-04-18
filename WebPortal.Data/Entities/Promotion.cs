using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class Promotion : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public bool? ApplyForAll { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string ApplyForProductIDs { get; set; }
        public string ApplyForCategories { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? WebisteID { get; set; }
    }
}
