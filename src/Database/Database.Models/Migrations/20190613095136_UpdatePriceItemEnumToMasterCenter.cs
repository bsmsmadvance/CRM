using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdatePriceItemEnumToMasterCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.AddColumn<Guid>(
                name: "PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SqaureWa",
                schema: "PRJ",
                table: "Project",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rai",
                schema: "PRJ",
                table: "Project",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ngan",
                schema: "PRJ",
                table: "Project",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "PriceTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "PriceUnitMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "PriceTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "PriceUnitMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "PriceTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "PriceUnitMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPriceItem_PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "PriceTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "PriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "PriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "PriceUnitMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "PriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "PriceUnitMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "PriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "PriceUnitMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_MasterCenter_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_MasterCenter_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPriceItem_PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceTypeMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitMasterCenterID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceTypeMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitMasterCenterID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitMasterCenterID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceTypeMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SqaureWa",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rai",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Ngan",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: 0);
        }
    }
}
