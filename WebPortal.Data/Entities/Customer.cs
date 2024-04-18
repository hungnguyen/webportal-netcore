using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.Data.Entities
{
    public class Customer : IEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public Status Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool? IsOnline { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public int? WebsiteID { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<ProductVote> ProductVotes { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
