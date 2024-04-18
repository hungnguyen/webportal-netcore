using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class ForgotPasswordRequest
    {

        [Required]
        public string Email { get; set; }
    }
}
