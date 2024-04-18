using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class PageViewConfiguration : IEntityTypeConfiguration<PageView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PageView> builder)
        {
            builder.ToTable("PageView");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
        }
    }
}
