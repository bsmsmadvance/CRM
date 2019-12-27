using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddRefIdToDepositHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferentID",
                schema: "FIN",
                table: "DepositHeader",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositHeader_ReferentID",
                schema: "FIN",
                table: "DepositHeader",
                column: "ReferentID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositHeader_DepositHeader_ReferentID",
                schema: "FIN",
                table: "DepositHeader",
                column: "ReferentID",
                principalSchema: "FIN",
                principalTable: "DepositHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositHeader_DepositHeader_ReferentID",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropIndex(
                name: "IX_DepositHeader_ReferentID",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "ReferentID",
                schema: "FIN",
                table: "DepositHeader");
        }
    }
}
