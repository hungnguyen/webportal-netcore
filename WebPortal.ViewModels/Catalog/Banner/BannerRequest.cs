using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class BannerRequest
    {
        public string Image { get; set; }
        public Status Status { get; set; }
        public string Link { get; set; }
        [Required]
        public string Name { get; set; }
        public Position Position { get; set; }

        [Display(Name= "In categories")]
        public string InCategories { get; set; }

        [Display(Name = "Order number")]
        public int OrderNumber { get; set; }
        public string LanguageID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Detail { get; set; }
        public int? WebsiteID { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }
    }
}
