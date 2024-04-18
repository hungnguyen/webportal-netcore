using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class OrderRequest
    {
        public OrderStatus OrderStatus { get; set; }
        public string PromotionCode { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmout { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalNetAmount { get; set; }
        public PayMethod PayMethod { get; set; }
        public PayStatus PayStatus { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingEmail { get; set; }
        public string FindUs { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? SaleID { get; set; }
        public int? CustomerID { get; set; }
        public int? WebsiteID { get; set; }
        public string SpecialRequest { get; set; }
    }
}
