using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LeadScore",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadSuperVisor",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadType",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lead_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                column: "PRJ_ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "LeadScore",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadSuperVisor",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadType",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                schema: "CTM",
                table: "Contact");
        }
    }
}
