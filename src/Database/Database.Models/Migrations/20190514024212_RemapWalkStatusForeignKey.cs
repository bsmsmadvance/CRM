using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemapWalkStatusForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "WalkStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "WalkStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.AddColumn<Guid>(
                name: "WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "WalkStatusCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_WalkStatusCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "WalkStatusCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
