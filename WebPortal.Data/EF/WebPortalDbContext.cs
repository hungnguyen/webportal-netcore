using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Configurations;
using WebPortal.Data.Entities;
using WebPortal.Data.Extensions;

namespace WebPortal.Data.EF
{
    public class WebPortalDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public WebPortalDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new MailBoxConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PageViewConfiguration());
            modelBuilder.ApplyConfiguration(new PhraseConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductFileConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVoteConfiguration());
            modelBuilder.ApplyConfiguration(new WebsiteConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaim");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin").HasKey(x=>x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserToken").HasKey(x=>x.UserId);

            modelBuilder.Seed();

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MailBox> MailBoxes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PageView> PageViews { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVote> ProductVotes { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
