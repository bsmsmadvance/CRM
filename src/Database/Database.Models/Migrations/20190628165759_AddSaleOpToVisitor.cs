using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddSaleOpToVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "SalesOpportunityMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "SalesOpportunityMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "SalesOpportunityMasterCenterID",
                schema: "CTM",
                table: "Visitor");
        }
    }
}
