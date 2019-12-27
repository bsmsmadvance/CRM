using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyTitleContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Contact_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "ContactStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_ContactStatusMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "TitleMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_TitleTHMasterCenterID",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_ContactStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "ContactStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_ContactStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.RenameColumn(
                name: "ContactStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "ContactStatusCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_ContactStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_ContactStatusCenterID");

            migrationBuilder.RenameColumn(
                name: "TitleMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "TitleTHMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_TitleMasterCenterID",
                schema: "CTM",
                table: "Contact",
                newName: "IX_Contact_TitleTHMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleENMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleENMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "ContactStatusCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
