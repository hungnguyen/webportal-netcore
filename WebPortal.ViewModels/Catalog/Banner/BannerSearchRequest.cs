using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class BannerSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
        public int? CategoryID { get; set; }
        public Status? Status { get; set; }
        public Position? Position { get; set; }
    }
}
