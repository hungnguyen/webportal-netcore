using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class MailBoxConfiguration : IEntityTypeConfiguration<MailBox>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MailBox> builder)
        {
            builder.ToTable("MailBox");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.FromEmail).HasMaxLength(250);
            builder.Property(x => x.ToEmail).HasMaxLength(250);
            builder.Property(x => x.Cc).HasMaxLength(250);
            builder.Property(x => x.Bcc).HasMaxLength(250);
            builder.Property(x => x.Subject).HasMaxLength(500);
            builder.Property(x => x.Body).HasColumnType("ntext");
            builder.Property(x => x.LanguageID).HasMaxLength(5);
        }
    }
}
