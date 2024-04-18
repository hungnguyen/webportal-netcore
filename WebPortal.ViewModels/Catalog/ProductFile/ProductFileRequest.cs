using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ProductFileRequest
    {
        [Required]
        public string Name { get; set; }
        public string Detail { get; set; }
        public string FileName { get; set; }
        public string Link { get; set; }

        [Display(Name = "Order number")]
        public int OrderNumber { get; set; }
        public int? ProductID { get; set; }
        public string FileUrl { get; set; }

        [Display(Name = "File")]
        public IFormFile NewFile { get; set; }
    }
}
