using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class Product : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public int? OrderNumber { get; set; }
        public string Image { get; set; }
        public string UrlName { get; set; }
        public int? ViewCount { get; set; }
        public int? LikeCount { get; set; }
        public string TypeCode { get; set; }
        public string LanguageID { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsFeature { get; set; }
        public string ReplateProduct { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
        public string Text6 { get; set; }
        public string Text7 { get; set; }
        public string Text8 { get; set; }
        public string Text9 { get; set; }
        public string Text10 { get; set; }
        public string Text11 { get; set; }
        public string Text12 { get; set; }
        public string Text13 { get; set; }
        public string Text14 { get; set; }
        public string Text15 { get; set; }
        public string Text16 { get; set; }
        public string Text17 { get; set; }
        public string Text18 { get; set; }
        public string Text19 { get; set; }
        public string Text20 { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Desc4 { get; set; }
        public string Desc5 { get; set; }
        public string Desc6 { get; set; }
        public string Desc7 { get; set; }
        public string Desc8 { get; set; }
        public string Desc9 { get; set; }
        public string Desc10 { get; set; }
        public int? WebsiteID { get; set; }
        public List<ProductFile> ProductFiles { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<ProductVote> ProductVotes { get; set; }
        public List<Cart> Carts { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
