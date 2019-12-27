using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_ProjectNameEN_Receipttempheader_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectNameEN",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectNameEN",
                schema: "FIN",
                table: "ReceiptTempHeader");
        }
    }
}
