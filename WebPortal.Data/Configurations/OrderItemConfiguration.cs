using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(x => x.Order).WithMany(y => y.OrderItems).HasForeignKey(x => x.OrderID);
            builder.HasOne(x => x.Product).WithMany(p => p.OrderItems).HasForeignKey(ca => ca.ProductID);
        }
    }
}
