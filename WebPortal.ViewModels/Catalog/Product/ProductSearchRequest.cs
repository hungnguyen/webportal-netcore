using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class ProductSearchRequest : SearchRequest
    {
        public string TypeCode { get; set; }
        public int CategoryID { get; set; }
        public string Keyword { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsFeature { get; set; }
        public Status? Status { get; set; }
        public ProductOrder Order { get; set; }
        public List<int> Ids { get; set; }
    }
}
