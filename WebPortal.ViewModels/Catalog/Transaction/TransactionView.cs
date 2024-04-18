using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class TransactionView
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ExternalID { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public string Provider { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
