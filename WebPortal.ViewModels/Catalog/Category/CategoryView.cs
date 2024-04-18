using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class CategoryView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }
        public string TypeCode { get; set; }
        public bool? IsPopular { get; set; }
        public string Image { get; set; }
        public string UrlName { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string ShortDescription { get; set; }
        public string DisplayType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
