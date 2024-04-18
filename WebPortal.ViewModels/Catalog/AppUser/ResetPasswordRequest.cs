using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ResetPasswordRequest
    {
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "New password doesn't match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
