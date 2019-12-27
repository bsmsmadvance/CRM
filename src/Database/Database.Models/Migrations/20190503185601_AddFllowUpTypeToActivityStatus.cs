using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFllowUpTypeToActivityStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowUpType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowUpType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.AlterColumn<string>(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
