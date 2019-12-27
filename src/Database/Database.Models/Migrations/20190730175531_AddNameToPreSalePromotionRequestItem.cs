using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddNameToPreSalePromotionRequestItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameTH",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEN",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "NameTH",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");
        }
    }
}
