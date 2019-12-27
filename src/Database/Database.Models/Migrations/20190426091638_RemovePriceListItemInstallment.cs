using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemovePriceListItemInstallment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListInstallmentItem",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "QuotationUnitPriceInstallmentItem",
                schema: "SAL");

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstallmentAmounts",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstallments",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentAmount",
                schema: "PRJ",
                table: "PriceListItem",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstallmentAmounts",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstallments",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "SpecialInstallmentAmounts",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "SpecialInstallments",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentAmount",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "SpecialInstallmentAmounts",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "SpecialInstallments",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.CreateTable(
                name: "PriceListInstallmentItem",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    PriceListItemID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListInstallmentItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListInstallmentItem_PriceListItem_PriceListItemID",
                        column: x => x.PriceListItemID,
                        principalSchema: "PRJ",
                        principalTable: "PriceListItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationUnitPriceInstallmentItem",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    QuotationUnitPriceItemID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationUnitPriceInstallmentItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationUnitPriceInstallmentItem_QuotationUnitPriceItem_QuotationUnitPriceItemID",
                        column: x => x.QuotationUnitPriceItemID,
                        principalSchema: "SAL",
                        principalTable: "QuotationUnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListInstallmentItem_PriceListItemID",
                schema: "PRJ",
                table: "PriceListInstallmentItem",
                column: "PriceListItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceInstallmentItem_QuotationUnitPriceItemID",
                schema: "SAL",
                table: "QuotationUnitPriceInstallmentItem",
                column: "QuotationUnitPriceItemID");
        }
    }
}
