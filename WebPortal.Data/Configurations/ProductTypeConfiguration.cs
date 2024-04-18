using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductType");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.Code).HasMaxLength(3);
            builder.Property(x => x.LanguageID).HasMaxLength(5);
            builder.Property(x => x.Text1).HasMaxLength(250);
            builder.Property(x => x.Text2).HasMaxLength(250);
            builder.Property(x => x.Text3).HasMaxLength(250);
            builder.Property(x => x.Text4).HasMaxLength(250);
            builder.Property(x => x.Text5).HasMaxLength(250);
            builder.Property(x => x.Text6).HasMaxLength(250);
            builder.Property(x => x.Text7).HasMaxLength(250);
            builder.Property(x => x.Text8).HasMaxLength(250);
            builder.Property(x => x.Text9).HasMaxLength(250);
            builder.Property(x => x.Text10).HasMaxLength(250);
            builder.Property(x => x.Text11).HasMaxLength(250);
            builder.Property(x => x.Text12).HasMaxLength(250);
            builder.Property(x => x.Text13).HasMaxLength(250);
            builder.Property(x => x.Text14).HasMaxLength(250);
            builder.Property(x => x.Text15).HasMaxLength(250);
            builder.Property(x => x.Text16).HasMaxLength(250);
            builder.Property(x => x.Text17).HasMaxLength(250);
            builder.Property(x => x.Text18).HasMaxLength(250);
            builder.Property(x => x.Text19).HasMaxLength(250);
            builder.Property(x => x.Text20).HasMaxLength(250);
            builder.Property(x => x.Desc1).HasMaxLength(250);
            builder.Property(x => x.Desc2).HasMaxLength(250);
            builder.Property(x => x.Desc3).HasMaxLength(250);
            builder.Property(x => x.Desc4).HasMaxLength(250);
            builder.Property(x => x.Desc5).HasMaxLength(250);
            builder.Property(x => x.Desc6).HasMaxLength(250);
            builder.Property(x => x.Desc7).HasMaxLength(250);
            builder.Property(x => x.Desc8).HasMaxLength(250);
            builder.Property(x => x.Desc9).HasMaxLength(250);
            builder.Property(x => x.Desc10).HasMaxLength(250);
        }
    }
}
