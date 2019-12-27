using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyContactStatusInVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.RenameColumn(
                name: "WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "VisitorWalkStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_VisitorWalkStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VisitorWalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitorWalkStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitorWalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.RenameColumn(
                name: "VisitorWalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "WalkStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_VisitorWalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_WalkStatusMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_PersonalVisitTypeMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "PersonalVisitTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitToStatusMasterCenterID");

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
                name: "FK_Visitor_MasterCenter_VisitToStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitToStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
    }
}
