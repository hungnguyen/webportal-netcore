using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class Language : IEntity
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int? WebsiteID { get; set; }
    }
}
