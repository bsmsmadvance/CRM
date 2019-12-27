using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeMasterPriceItemKeyToID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_MasterPriceItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItemTemplate_MasterPriceItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterPriceItem",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterPriceItem",
                schema: "MST",
                table: "MasterPriceItem",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "MasterPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "MasterPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "MasterPriceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_MasterPriceItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItemTemplate_MasterPriceItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_MasterPriceItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItemTemplate_MasterPriceItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterPriceItem",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "ID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterPriceItem",
                schema: "MST",
                table: "MasterPriceItem",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "MasterPriceItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "MasterPriceItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem",
                column: "MasterPriceItemKey");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_MasterPriceItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItemTemplate_MasterPriceItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
