using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeExpireDateRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                schema: "USR",
                table: "RefreshToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                schema: "USR",
                table: "RefreshToken",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "USR",
                table: "RefreshToken");

            migrationBuilder.AddColumn<long>(
                name: "Expires",
                schema: "USR",
                table: "RefreshToken",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
