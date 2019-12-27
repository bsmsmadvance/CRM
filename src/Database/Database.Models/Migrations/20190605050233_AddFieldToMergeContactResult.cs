using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFieldToMergeContactResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewContactID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OldContactID",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewContactID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "OldContactID",
                schema: "DMT",
                table: "MergeContactResult");
        }
    }
}
