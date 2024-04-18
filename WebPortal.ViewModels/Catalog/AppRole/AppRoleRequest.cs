using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class AppRoleRequest
    {
        [Required]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
    }
}
