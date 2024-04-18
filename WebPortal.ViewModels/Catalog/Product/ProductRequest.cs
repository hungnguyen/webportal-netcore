using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }
        public string Image { get; set; }
        [Display(Name = "Url Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UrlName { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public string TypeCode { get; set; }
        public string LanguageID { get; set; }
        [Display(Name = "Is Hot")]
        public bool IsHot { get; set; }
        [Display(Name = "Is Feature")]
        public bool IsFeature { get; set; }
        [Display(Name = "Replate Product")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ReplateProduct { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text3 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text4 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text5 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text6 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text7 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text8 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text9 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text10 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text11 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text12 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text13 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text14 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text15 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text16 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text17 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text18 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text19 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text20 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc3 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc4 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc5 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc6 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc7 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc8 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc9 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc10 { get; set; }
        public int? WebsiteID { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }
        [Display(Name = "In categories")]
        public string InCategories { get; set; }
    }
}
