using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class CategorySearchRequest : SearchRequest
    {
        public int? ParentID { get; set; }
        public string Keyword { get; set; }
        public string TypeCode { get; set; }
        public bool? IsOnTop { get; set; }
        public bool? IsOnRight { get; set; }
        public bool? IsOnBottom { get; set; }
        public bool? IsOnLeft { get; set; }
        public bool? IsOnCenter { get; set; }
        public Status? Status { get; set; }
        public int[] InIds { get; set; }
    }
}
