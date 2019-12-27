using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateMasterPriceItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceItemKey",
                schema: "MST");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
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

            migrationBuilder.CreateTable(
                name: "MasterPriceItem",
                schema: "MST",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PriceType = table.Column<int>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    ACCode = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DetailEN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPriceItem", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
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
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_UnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropTable(
                name: "MasterPriceItem",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceItemKey",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ACCode = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DetailEN = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    PriceType = table.Column<int>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceItemKey", x => x.ID);
                });
        }
    }
}
