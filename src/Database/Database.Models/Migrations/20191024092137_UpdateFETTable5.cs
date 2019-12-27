using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateFETTable5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactID",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FET_ContactID",
                schema: "FIN",
                table: "FET",
                column: "ContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_FET_Contact_ContactID",
                schema: "FIN",
                table: "FET",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FET_Contact_ContactID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropIndex(
                name: "IX_FET_ContactID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "ContactID",
                schema: "FIN",
                table: "FET");
        }
    }
}
