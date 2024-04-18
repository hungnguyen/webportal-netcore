using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class PromotionRequest
    {
        [Required]
        public string Name { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Apply For All")]        
        public bool ApplyForAll { get; set; }
        [Display(Name = "Discount Percent")]
        public int? DiscountPercent { get; set; }
        [Display(Name = "Discount Amount")]
        public decimal? DiscountAmount { get; set; }
        [Display(Name = "Apply For ProductIDs")]
        public string ApplyForProductIDs { get; set; }
        [Display(Name = "Apply For Categories")]
        public string ApplyForCategories { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        public int? WebisteID { get; set; }
    }
}
