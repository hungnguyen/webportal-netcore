using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Entities;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class OrderView
    {
        public int ID { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string PromotionCode { get; set; }
        public decimal Discount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal TotalAmout { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal Fee { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal TotalNetAmount { get; set; }
        public PayMethod PayMethod { get; set; }
        public PayStatus PayStatus { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingEmail { get; set; }
        public string FindUs { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCompleted { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateUpdate { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? SaleID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string SaleName { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItemView> OrderItems { get; set; }
    }
}
