using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class ProductTypeView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsPublic { get; set; }
        public Status Status { get; set; }
    }
}
