using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class PhraseRequest
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
        public string LanguageID { get; set; }
        [Display(Name = "Is pin?")]
        public bool IsPin { get; set; }
        public int? WebsiteID { get; set; }
    }
}
