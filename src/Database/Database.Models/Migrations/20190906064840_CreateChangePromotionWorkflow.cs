using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateChangePromotionWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ChangePromotionWorkflow_PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "PromotionTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangePromotionWorkflow_MasterCenter_PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "PromotionTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangePromotionWorkflow_MasterCenter_PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangePromotionWorkflow_PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "ApproveDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "PromotionTypeMasterCenterID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow");
        }
    }
}
