using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
