using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class SplitInstallmentPriceListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentPeriod",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentPeriodKey",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsSpecialInstallmentPeriod",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentPeriod",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentPeriodKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsSpecialInstallmentPeriod",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentPeriod",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "InstallmentPeriodKey",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "IsSpecialInstallmentPeriod",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.AddColumn<string>(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceListInstallmentItem",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PriceListItemID = table.Column<Guid>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
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
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuotationUnitPriceItemID = table.Column<Guid>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UnitPriceInstallmentItem",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UnitPriceItemID = table.Column<Guid>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPriceInstallmentItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitPriceInstallmentItem_UnitPriceItem_UnitPriceItemID",
                        column: x => x.UnitPriceItemID,
                        principalSchema: "SAL",
                        principalTable: "UnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemKey");

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

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceInstallmentItem_UnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceInstallmentItem",
                column: "UnitPriceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "MasterPriceItemKey",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_MasterPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropTable(
                name: "PriceListInstallmentItem",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "QuotationUnitPriceInstallmentItem",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "UnitPriceInstallmentItem",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentPeriod",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstallmentPeriodKey",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialInstallmentPeriod",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentPeriod",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstallmentPeriodKey",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialInstallmentPeriod",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentPeriod",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstallmentPeriodKey",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialInstallmentPeriod",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: false);
        }
    }
}
