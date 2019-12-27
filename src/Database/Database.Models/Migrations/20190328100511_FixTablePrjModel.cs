using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixTablePrjModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "Style",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterPowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<double>(
                name: "WaterPowerMeter",
                schema: "PRJ",
                table: "Model",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "PowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<double>(
                name: "PowerMeter",
                schema: "PRJ",
                table: "Model",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Frontage",
                schema: "PRJ",
                table: "Model",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Model_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "CategoryMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "StyleMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "TypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "CategoryMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "StyleMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "TypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "StyleMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "TypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterPowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "WaterPowerMeter",
                schema: "PRJ",
                table: "Model",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PowerMeter",
                schema: "PRJ",
                table: "Model",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Frontage",
                schema: "PRJ",
                table: "Model",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "PRJ",
                table: "Model",
                nullable: true);
        }
    }
}
