using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebPortal.ViewModels
{
    public class AppUserView
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime LastLoginDate { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
