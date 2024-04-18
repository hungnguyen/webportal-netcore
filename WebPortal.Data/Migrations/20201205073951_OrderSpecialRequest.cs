using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPortal.Data.Migrations
{
    public partial class OrderSpecialRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequest",
                table: "Order",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("3681895d-27fa-41f9-9207-3ea142634e51"), "ec1c947e-b1a6-46fa-9a35-8209b1cac2ed", null, "Administrator", "administrator" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "Browser", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IP", "Image", "IsOnline", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8a1f4ab3-a68e-49c5-a41f-8720f895f1ca"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5f109624-688e-420e-820d-ec10319bc81e", "hung.nguyen1610@gmail.com", true, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "hung.nguyen1610@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEAE9E6ZlENhBDvrO3aYLvvZbVfPRHvGOTwjIa3MlyNqdNrwYptJRNvQfclU4UxiYjQ==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("8a1f4ab3-a68e-49c5-a41f-8720f895f1ca"), new Guid("3681895d-27fa-41f9-9207-3ea142634e51") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("3681895d-27fa-41f9-9207-3ea142634e51"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("8a1f4ab3-a68e-49c5-a41f-8720f895f1ca"));

            migrationBuilder.DeleteData(
                table: "AppUserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("8a1f4ab3-a68e-49c5-a41f-8720f895f1ca"), new Guid("3681895d-27fa-41f9-9207-3ea142634e51") });

            migrationBuilder.DropColumn(
                name: "SpecialRequest",
                table: "Order");

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
    }
}
