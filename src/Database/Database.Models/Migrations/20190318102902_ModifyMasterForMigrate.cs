using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyMasterForMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "UnitNo");

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                schema: "MST",
                table: "PriceItemKey",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailEN",
                schema: "MST",
                table: "PriceItemKey",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                schema: "MST",
                table: "PriceItemKey");

            migrationBuilder.DropColumn(
                name: "DetailEN",
                schema: "MST",
                table: "PriceItemKey");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.RenameColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "UnitNumber");
        }
    }
}
