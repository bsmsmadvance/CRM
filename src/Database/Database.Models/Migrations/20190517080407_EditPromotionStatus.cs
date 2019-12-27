using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class EditPromotionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: false);
        }
    }
}
