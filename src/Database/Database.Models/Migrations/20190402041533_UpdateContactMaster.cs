using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateContactMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadType",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "National",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                newName: "PhoneTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPhone_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                newName: "IX_ContactPhone_PhoneTypeMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_NationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "NationalMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_NationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "NationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeMasterCenterID",
                schema: "CTM",
                table: "ContactPhone",
                column: "PhoneTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_MasterCenter_LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_NationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeMasterCenterID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Contact_NationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadTypeMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "NationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "PhoneTypeMasterCenterID",
                schema: "CTM",
                table: "ContactPhone",
                newName: "PhoneTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPhone_PhoneTypeMasterCenterID",
                schema: "CTM",
                table: "ContactPhone",
                newName: "IX_ContactPhone_PhoneTypeID");

            migrationBuilder.AddColumn<string>(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadType",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "National",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                column: "PhoneTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
