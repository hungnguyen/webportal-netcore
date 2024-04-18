using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.AdminPage.Models
{
    public class HomeViewModel
    {
        public int TotalProduct { get; set; }
        public int TotalBooking { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalNews { get; set; }
        public PagedResult<ProductView> InternalNews { get; set; }
        public PagedResult<ProductCommentView> ProductComments { get; set; }
        public PagedResult<OrderView> ListOrder { get; set; }
        public PagedResult<ProductView> ListProduct { get; set; }
    }
}
