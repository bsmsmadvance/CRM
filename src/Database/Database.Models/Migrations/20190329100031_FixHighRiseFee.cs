using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixHighRiseFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "CalculateArea",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceUsageArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePricePoolArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceBalconyArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceAirArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<Guid>(
                name: "CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "CalculateParkAreaMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_ProjectID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "TowerID");

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_MasterCenter_CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "CalculateParkAreaMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Floor_FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "FloorID",
                principalSchema: "PRJ",
                principalTable: "Floor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Project_ProjectID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Tower_TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "TowerID",
                principalSchema: "PRJ",
                principalTable: "Tower",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_MasterCenter_CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Floor_FloorID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Project_ProjectID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Tower_TowerID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_FloorID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_ProjectID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_TowerID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "CalculateParkAreaMasterCenterID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceUsageArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePricePoolArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceBalconyArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceAirArea",
                schema: "PRJ",
                table: "HighRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CalculateArea",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
