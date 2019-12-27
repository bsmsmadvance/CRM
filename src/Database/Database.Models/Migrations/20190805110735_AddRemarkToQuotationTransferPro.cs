using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddRemarkToQuotationTransferPro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 5000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion");
        }
    }
}
