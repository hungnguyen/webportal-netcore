using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class PhraseConfiguration : IEntityTypeConfiguration<Phrase>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Phrase> builder)
        {
            builder.ToTable("Phrase");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Key).HasMaxLength(250);
            builder.Property(x => x.Value).HasMaxLength(4000);
            builder.Property(x => x.LanguageID).HasMaxLength(5);
        }
    }
}
