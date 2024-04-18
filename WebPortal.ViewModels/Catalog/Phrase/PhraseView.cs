using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class PhraseView
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool? IsPin { get; set; }
    }
}
