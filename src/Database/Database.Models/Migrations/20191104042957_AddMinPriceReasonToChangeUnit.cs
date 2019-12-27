using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMinPriceReasonToChangeUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherMinPriceRequestReason",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "MinPriceRequestReasonMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeUnitWorkflow_MasterCenter_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "MinPriceRequestReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeUnitWorkflow_MasterCenter_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangeUnitWorkflow_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "OtherMinPriceRequestReason",
                schema: "SAL",
                table: "ChangeUnitWorkflow");
        }
    }
}
