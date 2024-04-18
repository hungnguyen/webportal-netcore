using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPortal.Data.Migrations
{
    public partial class ProductFileStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("155407e9-e21f-42c6-a92b-027bf51074b2"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"));

            migrationBuilder.DeleteData(
                table: "AppUserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"), new Guid("155407e9-e21f-42c6-a92b-027bf51074b2") });

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductFile",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "ID",
                keyValue: 1,
                column: "WebsiteID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "ID",
                keyValue: 2,
                column: "WebsiteID",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductFile");

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("155407e9-e21f-42c6-a92b-027bf51074b2"), "a5d6bd6c-cf3f-40fe-8958-2e420f9df576", null, "Administrator", "administrator" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "Browser", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IP", "Image", "IsOnline", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fccd54ad-e66c-4e66-aad4-f23d1800802c", "hung.nguyen1610@gmail.com", true, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "hung.nguyen1610@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEF5+aZvm1lau33yWOfrnuKRH3ScOGLhdneqa+6tT1aLmx5Hd0MdOOUPmm7+45RRTmA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"), new Guid("155407e9-e21f-42c6-a92b-027bf51074b2") });

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "ID",
                keyValue: 1,
                column: "WebsiteID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "ID",
                keyValue: 2,
                column: "WebsiteID",
                value: null);
        }
    }
}
