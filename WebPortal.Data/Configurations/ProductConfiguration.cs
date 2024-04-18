using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.CreatedBy).HasMaxLength(50);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.UrlName).HasMaxLength(250);
            builder.Property(x => x.TypeCode).HasMaxLength(3);
            builder.Property(x => x.LanguageID).HasMaxLength(5);
            builder.Property(x => x.ReplateProduct).HasMaxLength(250);
            builder.Property(x => x.Text1).HasMaxLength(1000);
            builder.Property(x => x.Text2).HasMaxLength(1000);
            builder.Property(x => x.Text3).HasMaxLength(1000);
            builder.Property(x => x.Text4).HasMaxLength(1000);
            builder.Property(x => x.Text5).HasMaxLength(1000);
            builder.Property(x => x.Text6).HasMaxLength(1000);
            builder.Property(x => x.Text7).HasMaxLength(1000);
            builder.Property(x => x.Text8).HasMaxLength(1000);
            builder.Property(x => x.Text9).HasMaxLength(1000);
            builder.Property(x => x.Text10).HasMaxLength(1000);
            builder.Property(x => x.Text11).HasMaxLength(1000);
            builder.Property(x => x.Text12).HasMaxLength(1000);
            builder.Property(x => x.Text13).HasMaxLength(1000);
            builder.Property(x => x.Text14).HasMaxLength(1000);
            builder.Property(x => x.Text15).HasMaxLength(1000);
            builder.Property(x => x.Text16).HasMaxLength(1000);
            builder.Property(x => x.Text17).HasMaxLength(1000);
            builder.Property(x => x.Text18).HasMaxLength(1000);
            builder.Property(x => x.Text19).HasMaxLength(1000);
            builder.Property(x => x.Text20).HasMaxLength(1000);
            builder.Property(x => x.Desc1).HasColumnType("ntext");
            builder.Property(x => x.Desc2).HasColumnType("ntext");
            builder.Property(x => x.Desc3).HasColumnType("ntext");
            builder.Property(x => x.Desc4).HasColumnType("ntext");
            builder.Property(x => x.Desc5).HasColumnType("ntext");
            builder.Property(x => x.Desc6).HasColumnType("ntext");
            builder.Property(x => x.Desc7).HasColumnType("ntext");
            builder.Property(x => x.Desc8).HasColumnType("ntext");
            builder.Property(x => x.Desc9).HasColumnType("ntext");
            builder.Property(x => x.Desc10).HasColumnType("ntext");
            builder.Property(x => x.ViewCount).HasDefaultValue(0);
            builder.Property(x => x.LikeCount).HasDefaultValue(0);
        }
    }
}
