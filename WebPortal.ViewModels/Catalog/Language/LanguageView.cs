using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class LanguageView
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
