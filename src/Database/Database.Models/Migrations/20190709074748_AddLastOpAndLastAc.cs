using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddLastOpAndLastAc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RevisitActivityCount",
                schema: "CTM",
                table: "Opportunity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LastOpportunityID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpportunityCount",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity",
                column: "LastOpportunityActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LastOpportunityID",
                schema: "CTM",
                table: "Contact",
                column: "LastOpportunityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Opportunity_LastOpportunityID",
                schema: "CTM",
                table: "Contact",
                column: "LastOpportunityID",
                principalSchema: "CTM",
                principalTable: "Opportunity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_OpportunityActivity_LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity",
                column: "LastOpportunityActivityID",
                principalSchema: "CTM",
                principalTable: "OpportunityActivity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Opportunity_LastOpportunityID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_OpportunityActivity_LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Contact_LastOpportunityID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "LastOpportunityActivityID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "RevisitActivityCount",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "LastOpportunityID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OpportunityCount",
                schema: "CTM",
                table: "Contact");
        }
    }
}
