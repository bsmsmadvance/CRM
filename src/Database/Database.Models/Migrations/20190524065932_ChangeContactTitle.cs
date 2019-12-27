using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeContactTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "TitleMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "ContactTitleTHMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_TitleMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "IX_Contact_ContactTitleTHMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "ContactTitleENMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "ContactTitleENMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "ContactTitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactTitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "ContactTitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "TitleMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ContactTitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "IX_Contact_TitleMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_TitleMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
