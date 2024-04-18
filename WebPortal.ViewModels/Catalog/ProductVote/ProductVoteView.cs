using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ProductVoteView
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Score { get; set; }
        public int CusomterID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
    }
}
