using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddIsAutoPRToMaterialItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAutoPR",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseMasterInPR",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAutoPR",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsUseMasterInPR",
                schema: "PRM",
                table: "PromotionMaterialItem");
        }
    }
}
