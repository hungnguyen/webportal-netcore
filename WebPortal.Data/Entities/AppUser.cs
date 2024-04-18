using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public bool? IsOnline { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Note { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public List<Order> Orders { get; set; }
    }
}
