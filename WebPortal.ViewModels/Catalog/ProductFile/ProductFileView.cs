using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ProductFileView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string FileName { get; set; }
        public string Link { get; set; }
        public int OrderNumber { get; set; }
        public int ProductID { get; set; }
    }
}
