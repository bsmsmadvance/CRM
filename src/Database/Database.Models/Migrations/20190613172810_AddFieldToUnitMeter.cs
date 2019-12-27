using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFieldToUnitMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ElectrictMeterSaved",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectrictMeterTransferDateSaved",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaterMeterSaved",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaterMeterTransferDateSaved",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectrictMeterSaved",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectrictMeterTransferDateSaved",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterSaved",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterTransferDateSaved",
                schema: "PRJ",
                table: "Unit");
        }
    }
}
