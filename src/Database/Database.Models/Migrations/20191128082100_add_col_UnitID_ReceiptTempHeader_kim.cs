using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_UnitID_ReceiptTempHeader_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempHeader_Unit_UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempHeader_Unit_UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTempHeader_UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "FIN",
                table: "ReceiptTempHeader");
        }
    }
}
