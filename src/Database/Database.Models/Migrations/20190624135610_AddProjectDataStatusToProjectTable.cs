using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddProjectDataStatusToProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "AgreementDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "BudgetProDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "GeneralDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "MinPriceDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ModelDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "PictureDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "PriceListDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TitleDeedDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TowerDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TransferFeeDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "UnitDataStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "WaiveDataStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "AgreementDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "BudgetProDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "GeneralDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "MinPriceDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ModelDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "PictureDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "PriceListDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TitleDeedDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TowerDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "TransferFeeDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "UnitDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "WaiveDataStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "AgreementDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "BudgetProDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "GeneralDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "MinPriceDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ModelDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PictureDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PriceListDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TitleDeedDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TowerDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TransferFeeDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UnitDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WaiveDataStatusMasterCenterID",
                schema: "PRJ",
                table: "Project");
        }
    }
}
