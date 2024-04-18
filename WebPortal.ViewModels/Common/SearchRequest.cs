using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class SearchRequest
    {
        public string LanguageId { get; set; } = "vi-VN";
        public int WebsiteID { get; set; } = 1;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
