using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class Banner : IEntity
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public Status Status { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public string InCategories { get; set; }
        public int OrderNumber { get; set; }
        public string LanguageID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string Detail { get; set; }
        public int? WebsiteID { get; set; }
    }
}
