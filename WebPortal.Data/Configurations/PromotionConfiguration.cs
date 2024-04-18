using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotion");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.ApplyForProductIDs).HasMaxLength(500);
            builder.Property(x => x.ApplyForCategories).HasMaxLength(500);
        }
    }
}
