using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class Category : IEntity
    {
        public int ID { get; set; }
        public string  Name { get; set; }
        public int OrderNumber { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }
        public string TypeCode { get; set; }
        public bool? IsPopular { get; set; }
        public string Image { get; set; }
        public string DisplayType { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDescription { get; set; }
        public string UrlName { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string ShortDescription { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string LanguageID { get; set; }
        public bool? IsOnTop { get; set; }
        public bool? IsOnRight { get; set; }
        public bool? IsOnBottom { get; set; }
        public bool? IsOnLeft { get; set; }
        public bool? IsOnCenter { get; set; }
        public int? WebsiteID { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
