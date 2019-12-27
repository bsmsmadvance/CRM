using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFieldUnitMeterTranferOwnerShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransferOwnerShipDate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferOwnerShipDate",
                schema: "PRJ",
                table: "Unit");
        }
    }
}
