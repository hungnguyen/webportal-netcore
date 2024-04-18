using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Data.Migrations
{
    public partial class AddIsResetCache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "IsResetCache",
                table: "Website",
                type: "bit",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "IsResetCache",
                table: "Website");

            
        }
    }
}
