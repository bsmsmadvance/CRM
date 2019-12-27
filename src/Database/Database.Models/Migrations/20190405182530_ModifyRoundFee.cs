using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyRoundFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.AlterColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
