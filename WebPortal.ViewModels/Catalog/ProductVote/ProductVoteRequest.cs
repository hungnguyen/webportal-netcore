using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ProductVoteRequest
    {
        public int ProductID { get; set; }
        public int Score { get; set; }
        public int CusomterID { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
