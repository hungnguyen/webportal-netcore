using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class BannerView
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public Status Status { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        [Display(Name="Order number")]
        public int OrderNumber { get; set; }

        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Date updated")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
    }
}
