using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixFromDevelopBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Visitor_MasterCenter_VehicleCenterID",
            //    schema: "CTM",
            //    table: "Visitor");

            //migrationBuilder.DropIndex(
            //    name: "IX_Visitor_VehicleCenterID",
            //    schema: "CTM",
            //    table: "Visitor");

            //migrationBuilder.DropColumn(
            //    name: "VehicleCenterID",
            //    schema: "CTM",
            //    table: "Visitor");

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                schema: "PRJ",
                table: "Project",
                maxLength: 5000,
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Visitor_VehicleMasterCenterID",
            //    schema: "CTM",
            //    table: "Visitor",
            //    column: "VehicleMasterCenterID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Visitor_MasterCenter_VehicleMasterCenterID",
            //    schema: "CTM",
            //    table: "Visitor",
            //    column: "VehicleMasterCenterID",
            //    principalSchema: "MST",
            //    principalTable: "MasterCenter",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VehicleMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VehicleMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "DeleteReason",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VehicleCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VehicleCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
