using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.SessionID);
            builder.HasOne(ca => ca.Customer).WithMany(cu => cu.Carts).HasForeignKey(ca => ca.CustomerID);
            builder.HasOne(ca => ca.Product).WithMany(p => p.Carts).HasForeignKey(ca => ca.ProductID);
        }
    }
}
