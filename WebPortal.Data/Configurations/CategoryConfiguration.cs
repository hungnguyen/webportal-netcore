using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.TypeCode).HasMaxLength(3);
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.DisplayType).HasMaxLength(250);
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Description).HasColumnType("ntext");
            builder.Property(x => x.MetaTitle).HasMaxLength(500);
            builder.Property(x => x.MetaKey).HasMaxLength(1000);
            builder.Property(x => x.MetaDescription).HasMaxLength(1000);
            builder.Property(x => x.UrlName).HasMaxLength(250);
            builder.Property(x => x.Link).HasMaxLength(500);
            builder.Property(x => x.Icon).HasMaxLength(250);
            builder.Property(x => x.ShortDescription).HasMaxLength(1000);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.LanguageID).HasMaxLength(5);
        }
    }
}
