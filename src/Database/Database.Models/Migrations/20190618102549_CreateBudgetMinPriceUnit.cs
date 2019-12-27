using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateBudgetMinPriceUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetMinPriceUnit",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetMinPriceID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetMinPriceUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BudgetMinPriceUnit_BudgetMinPrice_BudgetMinPriceID",
                        column: x => x.BudgetMinPriceID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetMinPrice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetMinPriceUnit_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPriceUnit_BudgetMinPriceID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "BudgetMinPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPriceUnit_UnitID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetMinPriceUnit",
                schema: "PRJ");
        }
    }
}
