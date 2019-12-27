using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RevistCustomerTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.RenameColumn(
                name: "ContactType",
                schema: "CTM",
                table: "Lead",
                newName: "SubLeadType");

            migrationBuilder.AddColumn<Guid>(
                name: "RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLatestScore",
                schema: "CTM",
                table: "LeadScoring",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BuyReason",
                schema: "CTM",
                table: "Lead",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMailSendedToLC",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "RefVisitorRelationMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead",
                column: "CurrentLeadActivityStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_LeadActivityStatus_CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead",
                column: "CurrentLeadActivityStatusID",
                principalSchema: "CTM",
                principalTable: "LeadActivityStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "RefVisitorRelationMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_LeadActivityStatus_CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Lead_CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RefVisitorRelationMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IsLatestScore",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "BuyReason",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CurrentLeadActivityStatusID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "IsMailSendedToLC",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.RenameColumn(
                name: "SubLeadType",
                schema: "CTM",
                table: "Lead",
                newName: "ContactType");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: false);
        }
    }
}
