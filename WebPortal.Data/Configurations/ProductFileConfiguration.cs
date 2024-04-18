using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductFileConfiguration : IEntityTypeConfiguration<ProductFile>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductFile> builder)
        {
            builder.ToTable("ProductFile");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.FileName).HasMaxLength(250);
            builder.Property(x => x.Detail).HasMaxLength(1000);
            builder.Property(x => x.Link).HasMaxLength(500);
            builder.HasOne(pf => pf.Product).WithMany(p => p.ProductFiles).HasForeignKey(pc => pc.ProductID);
        }
    }
}
