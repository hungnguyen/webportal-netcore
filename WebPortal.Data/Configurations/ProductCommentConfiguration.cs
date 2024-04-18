using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable("ProductComment");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Subject).HasMaxLength(250);
            builder.Property(x => x.Message).HasMaxLength(250);
            builder.Property(x => x.PeopleLike).HasMaxLength(250);            
            builder.Property(x => x.LikeCount).HasDefaultValue(0);
            builder.HasOne(pc => pc.Product).WithMany(p => p.ProductComments).HasForeignKey(pc => pc.ProductID);
            builder.HasOne(pc => pc.Customer).WithMany(c => c.ProductComments).HasForeignKey(pc => pc.CustomerID);
        }
    }
}
