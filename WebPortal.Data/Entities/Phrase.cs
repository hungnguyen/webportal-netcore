using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class Phrase : IEntity
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string LanguageID { get; set; }
        public bool? IsPin { get; set; }
        public int? WebsiteID { get; set; }
    }
}
