using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateFETTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherMinPriceRequestReason",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BudgetMinPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BudgetMinPriceUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "MinPriceRequestReasonMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_BudgetMinPrice_BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BudgetMinPriceID",
                principalSchema: "PRJ",
                principalTable: "BudgetMinPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_BudgetMinPriceUnit_BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BudgetMinPriceUnitID",
                principalSchema: "PRJ",
                principalTable: "BudgetMinPriceUnit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "MinPriceRequestReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_BudgetMinPrice_BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_BudgetMinPriceUnit_BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "BudgetMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "BudgetMinPriceUnitID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "MinPriceRequestReasonMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "OtherMinPriceRequestReason",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");
        }
    }
}
