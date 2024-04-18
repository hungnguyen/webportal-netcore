using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPortal.Data.Migrations
{
    public partial class ProductQuality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("c156edda-6df7-4e52-a86a-31dd7727a8d2"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("5be5c26f-b8f4-48f9-bf9a-a301524d1a7f"));

            migrationBuilder.DeleteData(
                table: "AppUserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("5be5c26f-b8f4-48f9-bf9a-a301524d1a7f"), new Guid("c156edda-6df7-4e52-a86a-31dd7727a8d2") });

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "ProductComment",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("94c38e0c-0dde-4343-b887-b4e9973c9044"), "25c815c0-4102-4206-98d3-d38c5d15a07b", null, "Administrator", "administrator" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "Browser", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IP", "Image", "IsOnline", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("3c1a8967-a99d-4d10-b278-258777abe6bd"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "f0dd3846-f655-41ff-b9a7-e6047c051783", "hung.nguyen1610@gmail.com", true, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "hung.nguyen1610@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEGn/UAPsACYvl1B9cbou5PZ5P9cK7EgefDQLZjcW58e466ubfWeDC+bpC36v0W372w==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("3c1a8967-a99d-4d10-b278-258777abe6bd"), new Guid("94c38e0c-0dde-4343-b887-b4e9973c9044") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("94c38e0c-0dde-4343-b887-b4e9973c9044"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("3c1a8967-a99d-4d10-b278-258777abe6bd"));

            migrationBuilder.DeleteData(
                table: "AppUserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("3c1a8967-a99d-4d10-b278-258777abe6bd"), new Guid("94c38e0c-0dde-4343-b887-b4e9973c9044") });

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "ProductComment");

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("c156edda-6df7-4e52-a86a-31dd7727a8d2"), "4ed39bd4-c6d9-44b3-858b-4ecb1ecb6f6c", null, "Administrator", "administrator" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "Browser", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IP", "Image", "IsOnline", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5be5c26f-b8f4-48f9-bf9a-a301524d1a7f"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "bfcc1bdd-5f4c-4e8e-8e28-8b75e8db051c", "hung.nguyen1610@gmail.com", true, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "hung.nguyen1610@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEO06tK3RsbQ1ArRUPuZavw1AGgNCRkHfOfumN3kDRrBQMK3J6HRqKg5k85QxmDRMDQ==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("5be5c26f-b8f4-48f9-bf9a-a301524d1a7f"), new Guid("c156edda-6df7-4e52-a86a-31dd7727a8d2") });
        }
    }
}
