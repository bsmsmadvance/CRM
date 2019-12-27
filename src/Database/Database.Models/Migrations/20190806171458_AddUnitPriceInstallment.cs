using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddUnitPriceInstallment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentOfUnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsSpecialInstallment",
                schema: "SAL",
                table: "UnitPriceItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentOfUnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialInstallment",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);
        }
    }
}
