using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyWaterElectriceMeterPriceNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterElectricMeterPrice_Model_ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<decimal>(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterElectricMeterPrice_Model_ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ModelID",
                principalSchema: "PRJ",
                principalTable: "Model",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterElectricMeterPrice_Model_ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterElectricMeterPrice_Model_ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ModelID",
                principalSchema: "PRJ",
                principalTable: "Model",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
