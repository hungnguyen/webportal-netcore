using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class WebsiteConfiguration : IEntityTypeConfiguration<Website>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Website> builder)
        {
            builder.ToTable("Website");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();            
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.Domain).HasMaxLength(250);
            builder.Property(x => x.Folder).HasMaxLength(250);
            builder.Property(x => x.MobileFolder).HasMaxLength(250);
            builder.Property(x => x.DomainAlias).HasMaxLength(250);
            builder.Property(x => x.FromEmail).HasMaxLength(250);
            builder.Property(x => x.SMTPServer).HasMaxLength(50);
            builder.Property(x => x.SMTPServerPort).HasMaxLength(5);
            builder.Property(x => x.SMTPUserName).HasMaxLength(50);
            builder.Property(x => x.SMTPUserPassword).HasMaxLength(50);
            builder.Property(x => x.SMTPSSL).HasMaxLength(5);
            builder.Property(x => x.Currency).HasMaxLength(3);
            builder.Property(x => x.UploadFolder).HasMaxLength(250);
            builder.Property(x => x.ProjectName).HasMaxLength(250);
            builder.Property(x => x.ProjectLink).HasMaxLength(250);
            builder.Property(x => x.TotalPageView).HasDefaultValue(0);
            builder.Property(x => x.IsDown).HasDefaultValue(false);
        }
    }
}
