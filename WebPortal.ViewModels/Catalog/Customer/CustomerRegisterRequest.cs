using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class CustomerRegisterRequest
    {
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
