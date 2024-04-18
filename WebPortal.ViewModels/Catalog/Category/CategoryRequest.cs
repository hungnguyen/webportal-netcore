using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; }

        [Display(Name= "Order number")]
        public int OrderNumber { get; set; }
        [Display(Name = "Parent")]
        public int? ParentID { get; set; }
        public Status Status { get; set; }

        [Display(Name = "Type")]
        public string TypeCode { get; set; }

        [Display(Name = "Popular")]
        public bool IsPopular { get; set; }
        public string Image { get; set; }

        [Display(Name = "Display type")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DisplayType { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        [Display(Name = "Meta title")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta keyword")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MetaKey { get; set; }

        [Display(Name = "Meta description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MetaDescription { get; set; }

        [Display(Name = "Url name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UrlName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Link { get; set; }
        public string Icon { get; set; }

        [Display(Name = "Short description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ShortDescription { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string LanguageID { get; set; }

        [Display(Name = "Top")]
        public bool IsOnTop { get; set; }

        [Display(Name = "Right")]
        public bool IsOnRight { get; set; }

        [Display(Name = "Bottom")]
        public bool IsOnBottom { get; set; }

        [Display(Name = "Left")]
        public bool IsOnLeft { get; set; }

        [Display(Name = "Center")]
        public bool IsOnCenter { get; set; }
        public int? WebsiteID { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name="Image")]
        public IFormFile NewImage { get; set; }
        public string IconUrl { get; set; }

        [Display(Name = "Icon")]
        public IFormFile NewIcon { get; set; }
    }
}
