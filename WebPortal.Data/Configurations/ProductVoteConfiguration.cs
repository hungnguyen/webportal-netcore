using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Configurations
{
    public class ProductVoteConfiguration : IEntityTypeConfiguration<ProductVote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductVote> builder)
        {
            builder.ToTable("ProductVote");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(pv => pv.Product).WithMany(p => p.ProductVotes).HasForeignKey(pv => pv.ProductID);
            builder.HasOne(pv => pv.Customer).WithMany(p => p.ProductVotes).HasForeignKey(pv => pv.CusomterID);
        }
    }
}
