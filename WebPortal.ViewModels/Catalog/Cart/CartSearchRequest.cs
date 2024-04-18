using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class CartSearchRequest:SearchRequest
    {
        public int CustomerID { get; set; }
        public Guid SessionID { get; set; }
    }
}
