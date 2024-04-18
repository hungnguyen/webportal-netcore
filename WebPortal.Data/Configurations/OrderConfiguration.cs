using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.PromotionCode).HasMaxLength(50);
            builder.Property(x => x.ShippingAddress).HasMaxLength(500);
            builder.Property(x => x.ShippingEmail).HasMaxLength(250);
            builder.Property(x => x.ShippingName).HasMaxLength(250);
            builder.Property(x => x.ShippingPhone).HasMaxLength(50);
            builder.Property(x => x.FindUs).HasMaxLength(250);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.SpecialRequest).HasMaxLength(1000);
            builder.HasOne(o => o.Customer).WithMany(cu => cu.Orders).HasForeignKey(o => o.CustomerID);
            builder.HasOne(o => o.AppUser).WithMany(au => au.Orders).HasForeignKey(o => o.SaleID);
        }
    }
}
