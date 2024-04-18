using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategory");
            builder.HasKey(x => new { x.ProductID, x.CategoryID });
            builder.HasOne(pic => pic.Product).WithMany(p => p.ProductInCategories).HasForeignKey(pic => pic.ProductID);
            builder.HasOne(pic => pic.Category).WithMany(c => c.ProductInCategories).HasForeignKey(pic => pic.CategoryID);
        }
    }
}
