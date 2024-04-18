using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ExternalID).HasMaxLength(50);
            builder.Property(x => x.Status).HasMaxLength(250);
            builder.Property(x => x.Result).HasMaxLength(250);
            builder.Property(x => x.Provider).HasMaxLength(250);
            builder.Property(x => x.Message).HasMaxLength(1000);
            builder.HasOne(t => t.Order).WithMany(o => o.Transactions).HasForeignKey(t => t.OrderID);
            builder.HasOne(t => t.Customer).WithMany(c => c.Transactions).HasForeignKey(t => t.CustomerID);
        }
    }
}
