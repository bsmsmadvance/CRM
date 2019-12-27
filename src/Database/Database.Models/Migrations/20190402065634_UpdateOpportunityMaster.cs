using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateOpportunityMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateSalesOpportunity",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "SalesOpportunity",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.RenameColumn(
                name: "StatusQuestionaire",
                schema: "CTM",
                table: "Opportunity",
                newName: "LCOwner");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                schema: "CTM",
                table: "Lead",
                newName: "Firstname");

            migrationBuilder.AddColumn<Guid>(
                name: "EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "EstimateSalesOpportunityMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "SalesOpportunityMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "StatusQuestionaireMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_MasterCenter_EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "EstimateSalesOpportunityMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_MasterCenter_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "SalesOpportunityMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_MasterCenter_StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity",
                column: "StatusQuestionaireMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_MasterCenter_EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_MasterCenter_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_MasterCenter_StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "EstimateSalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "StatusQuestionaireMasterCenterID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.RenameColumn(
                name: "LCOwner",
                schema: "CTM",
                table: "Opportunity",
                newName: "StatusQuestionaire");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                schema: "CTM",
                table: "Lead",
                newName: "Fullname");

            migrationBuilder.AddColumn<string>(
                name: "EstimateSalesOpportunity",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesOpportunity",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);
        }
    }
}
