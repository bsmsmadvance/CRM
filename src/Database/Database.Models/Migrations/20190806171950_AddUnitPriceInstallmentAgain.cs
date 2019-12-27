using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddUnitPriceInstallmentAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitPriceInstallment",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    InstallmentOfUnitPriceItemID = table.Column<Guid>(nullable: true),
                    IsSpecialInstallment = table.Column<bool>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPriceInstallment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitPriceInstallment_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitPriceInstallment_UnitPriceItem_InstallmentOfUnitPriceItemID",
                        column: x => x.InstallmentOfUnitPriceItemID,
                        principalSchema: "SAL",
                        principalTable: "UnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitPriceInstallment_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceInstallment_CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceInstallment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceInstallment_InstallmentOfUnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceInstallment",
                column: "InstallmentOfUnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceInstallment_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceInstallment",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitPriceInstallment",
                schema: "SAL");
        }
    }
}
