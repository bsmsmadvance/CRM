using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddUnitMeterFieldsToUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDocumentDate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeterRemark",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeterStatus",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeterTopic",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricMeterTransferDate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransferElectricMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransferWaterMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeterRemark",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeterStatus",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeterTopic",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaterMeterTransferDate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDocumentDate",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeter",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterRemark",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterStatus",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterTopic",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterTransferDate",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsTransferElectricMeter",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsTransferWaterMeter",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeter",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterRemark",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterStatus",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterTopic",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterTransferDate",
                schema: "PRJ",
                table: "Unit");
        }
    }
}
