using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPreferStatusToTitledeedHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "PreferStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_MasterCenter_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "PreferStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_MasterCenter_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");
        }
    }
}
