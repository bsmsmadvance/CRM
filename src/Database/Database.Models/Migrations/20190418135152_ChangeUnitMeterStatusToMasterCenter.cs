using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeUnitMeterStatusToMasterCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricMeterStatus",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterTopic",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterStatus",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterTopic",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<Guid>(
                name: "ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterTopicMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterTopicMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "ElectricMeterTopicMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "WaterMeterTopicMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ElectricMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterTopicMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeterStatus",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricMeterTopic",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeterStatus",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterMeterTopic",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);
        }
    }
}
