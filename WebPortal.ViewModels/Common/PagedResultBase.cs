using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class PagedResultBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRow { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRow / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
