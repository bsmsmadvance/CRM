using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Fixlowrisefencefee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseType",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "LandOffice",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "IronRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "DepreciationPerYear",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConcreteRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "CalculateFence",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFenceFee_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFenceFee_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "ModelCategoryMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_MasterCenter_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "ModelCategoryMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_MasterCenter_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFenceFee_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFenceFee_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "IronRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DepreciationPerYear",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ConcreteRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CalculateFence",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseType",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandOffice",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);
        }
    }
}
