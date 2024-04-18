using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class LanguageRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int? WebsiteID { get; set; }
    }
}
