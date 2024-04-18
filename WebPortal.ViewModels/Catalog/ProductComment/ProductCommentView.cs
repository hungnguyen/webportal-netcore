using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class ProductCommentView
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }
        public int LikeCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }
        public int CustomerID { get; set; }
        public int Quality { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
    }
}
