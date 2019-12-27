using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateLeadActivityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityType",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "ActivityTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "ActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);
        }
    }
}
