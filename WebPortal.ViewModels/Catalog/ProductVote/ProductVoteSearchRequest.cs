using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class ProductVoteSearchRequest : SearchRequest
    {
        public int? ProductID { get; set; }
    }
}
