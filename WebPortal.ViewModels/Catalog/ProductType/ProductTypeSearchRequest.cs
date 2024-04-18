﻿using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class ProductTypeSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
        public bool? IsPublic { get; set; }
    }
}
