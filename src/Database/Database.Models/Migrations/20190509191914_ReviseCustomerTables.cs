using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseCustomerTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.RenameColumn(
                name: "VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "WelcomeLCUserID");

            migrationBuilder.RenameColumn(
                name: "Filename",
                schema: "CTM",
                table: "Visitor",
                newName: "VisitorRunning");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_WelcomeLCUserID");

            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                schema: "CTM",
                table: "LeadActivity",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                schema: "CTM",
                table: "Contact",
                newName: "WhatsAppID");

            migrationBuilder.RenameColumn(
                name: "Wechat",
                schema: "CTM",
                table: "Contact",
                newName: "WeChatID");

            migrationBuilder.AddColumn<string>(
                name: "IDCardImage",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDriver",
                schema: "CTM",
                table: "Visitor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalVisitImageFromCard",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RefVisitorID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitKioskDeviceID",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitKioskTransactionID",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorEmailAddress",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorFullAddress",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorFullName",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorFullNameEN",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorIDCardFullAddress",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorMobile",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorWorkingFullAddress",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CampaignID",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactType",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefID",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsThaiNationality",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "PersonalVisitCardTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "PersonalVisitTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitByMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitToStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "PersonalVisitCardTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "PersonalVisitTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitByMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitToStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_User_WelcomeLCUserID",
                schema: "CTM",
                table: "Visitor",
                column: "WelcomeLCUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_User_WelcomeLCUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IDCardImage",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IsDriver",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PersonalVisitCardTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PersonalVisitImageFromCard",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "RefVisitorID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitByMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitKioskDeviceID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitKioskTransactionID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorEmailAddress",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorFullAddress",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorFullName",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorFullNameEN",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorIDCardFullAddress",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorMobile",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitorWorkingFullAddress",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "CampaignID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "ContactType",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RefID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "IsThaiNationality",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "WelcomeLCUserID",
                schema: "CTM",
                table: "Visitor",
                newName: "VisitMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "VisitorRunning",
                schema: "CTM",
                table: "Visitor",
                newName: "Filename");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_WelcomeLCUserID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_VisitMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                schema: "CTM",
                table: "LeadActivity",
                newName: "ActivityDate");

            migrationBuilder.RenameColumn(
                name: "WhatsAppID",
                schema: "CTM",
                table: "Contact",
                newName: "WhatsApp");

            migrationBuilder.RenameColumn(
                name: "WeChatID",
                schema: "CTM",
                table: "Contact",
                newName: "Wechat");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
