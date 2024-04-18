using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class PromotionView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public bool ApplyForAll { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ToDate { get; set; }
    }
}
