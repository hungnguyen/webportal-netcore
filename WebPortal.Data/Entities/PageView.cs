using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class PageView : IEntity
    {
        public int ID { get; set; }
        public int? Total { get; set; }
        public DateTime? DateReported { get; set; }
        public int? WebsiteID { get; set; }
    }
}
