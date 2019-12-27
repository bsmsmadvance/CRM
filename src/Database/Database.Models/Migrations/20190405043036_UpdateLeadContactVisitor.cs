using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateLeadContactVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "ActualDate",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "ConvenientTime",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "StopTrack",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "StopTrackReason",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "ConvenientTime",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "Firstname",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "HomePhoneNumber",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Lastname",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "ContactStatusCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "ActivityTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "ConvenientTimeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "ConvenientTimeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_MasterCenter_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "ConvenientTimeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "ActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "ConvenientTimeMasterCenterID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_MasterCenter_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivity_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivity_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "ContactStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.AddColumn<string>(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDate",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConvenientTime",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StopTrack",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StopTrackReason",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConvenientTime",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePhoneNumber",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "CTM",
                table: "Lead",
                nullable: true);
        }
    }
}
