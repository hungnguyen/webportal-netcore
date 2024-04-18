using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class ProductFileSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
        public int? ProductID { get; set; }
    }
}
