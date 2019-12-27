using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeWaterElectricMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<Guid>(
                name: "ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterPriceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_WaterElectricMeterPrice_ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterPriceID",
                principalSchema: "PRJ",
                principalTable: "WaterElectricMeterPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_WaterElectricMeterPrice_WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterPriceID",
                principalSchema: "PRJ",
                principalTable: "WaterElectricMeterPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_WaterElectricMeterPrice_ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_WaterElectricMeterPrice_WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterPriceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<decimal>(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }
    }
}
