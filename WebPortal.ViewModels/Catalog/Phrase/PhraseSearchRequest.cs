﻿using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.ViewModels.Common;

namespace WebPortal.ViewModels
{
    public class PhraseSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
    }
}