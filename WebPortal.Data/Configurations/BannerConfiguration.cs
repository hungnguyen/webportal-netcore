using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("Banner");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.Link).HasMaxLength(500);
            builder.Property(x => x.InCategories).HasMaxLength(250);
            builder.Property(x => x.LanguageID).HasMaxLength(5);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.Detail).HasMaxLength(1000);
        }
    }
}
