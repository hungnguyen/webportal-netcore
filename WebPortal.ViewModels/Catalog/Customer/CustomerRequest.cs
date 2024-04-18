using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class CustomerRequest
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Id card")]
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public Status Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool? IsOnline { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public int? WebsiteID { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }
    }
}
