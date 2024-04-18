using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class ProductView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        [Display(Name = "Date created")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        [Display(Name = "Date updated")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Order number")]
        public int OrderNumber { get; set; }
        public string Image { get; set; }
        public string UrlName { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
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
    }
}
