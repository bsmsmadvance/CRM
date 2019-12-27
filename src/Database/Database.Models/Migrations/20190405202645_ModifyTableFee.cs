using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyTableFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_Unit_UnitID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
