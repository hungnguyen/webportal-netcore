using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.IP).HasMaxLength(15);
            builder.Property(x => x.Browser).HasMaxLength(500);
        }
    }
}
