using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class AppUserRequest
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        [Display(Name = "Lockout")]
        public bool LockoutEnabled { get; set; }
        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }
        [Display(Name = "Password")]
        public string NewPassword { get; set; }
        public List<string> InRole { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public bool? IsOnline { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string SecurityStamp { get; set; }
        public string ImageUrl { get; set; }
    }
}
