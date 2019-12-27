using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Fixtablelowrisefee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceArea",
                schema: "PRJ",
                table: "LowRiseFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFee_ProjectID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFee_UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_Project_ProjectID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_Project_ProjectID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFee_ProjectID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFee_UnitID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePriceArea",
                schema: "PRJ",
                table: "LowRiseFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitNo",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: true);
        }
    }
}
