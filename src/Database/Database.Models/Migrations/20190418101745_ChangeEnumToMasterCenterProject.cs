using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeEnumToMasterCenterProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProjectStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProjectStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ProjectStatus",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: 0);
        }
    }
}
