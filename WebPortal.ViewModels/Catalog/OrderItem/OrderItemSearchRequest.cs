using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class OrderItemSearchRequest : SearchRequest
    {
        public int OrderID { get; set; }
    }
}
