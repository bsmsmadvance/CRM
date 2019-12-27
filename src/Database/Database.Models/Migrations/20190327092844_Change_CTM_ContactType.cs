using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Change_CTM_ContactType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactType",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactTypeMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "ContactType",
                schema: "CTM",
                table: "Contact",
                nullable: true);
        }
    }
}
