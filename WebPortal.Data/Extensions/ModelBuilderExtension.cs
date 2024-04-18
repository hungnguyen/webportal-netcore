using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;

namespace WebPortal.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Language>().HasData(
                new Language() { ID=1, Code = "vi-VN", IsDefault = true, Name = "Tiếng Việt", WebsiteID=1 },
                new Language() { ID=2, Code = "en-US", IsDefault = false, Name = "English", WebsiteID = 1 }
                );

            builder.Entity<Website>().HasData(
                new Website() { ID = 1, Name = "DefaultWebsite" }
                );

            var UserId = Guid.NewGuid();
            var RoleId = Guid.NewGuid();
            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = UserId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    Email = "admin@domain.com",
                    NormalizedEmail = "admin@domain.com",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty
                });
            builder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = RoleId,
                    Name = "Administrator",
                    NormalizedName = "administrator"
                });

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>()
                {
                    UserId = UserId,
                    RoleId = RoleId
                });
        }
    }
}
