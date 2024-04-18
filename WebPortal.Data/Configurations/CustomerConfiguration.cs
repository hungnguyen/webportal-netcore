using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(250);
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.IdCard).HasMaxLength(50);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.Country).HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.City).HasMaxLength(250);
            builder.Property(x => x.District).HasMaxLength(250);
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.IP).HasMaxLength(15);
            builder.Property(x => x.Browser).HasMaxLength(500);
        }
    }
}
