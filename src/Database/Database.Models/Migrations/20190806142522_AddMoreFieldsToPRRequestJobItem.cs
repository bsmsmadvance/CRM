using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMoreFieldsToPRRequestJobItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameTH",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.AddColumn<string>(
                name: "ApproveName",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                schema: "PRM",
                table: "PRRequestJobItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ShortText",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveName",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "ShortText",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.AddColumn<string>(
                name: "NameTH",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 1000,
                nullable: true);
        }
    }
}
