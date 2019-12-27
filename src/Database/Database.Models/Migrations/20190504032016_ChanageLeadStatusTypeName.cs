using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChanageLeadStatusTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.AddColumn<int>(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);
        }
    }
}
