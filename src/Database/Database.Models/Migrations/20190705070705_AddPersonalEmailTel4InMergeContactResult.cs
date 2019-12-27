using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPersonalEmailTel4InMergeContactResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EMail",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel_4",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMail",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "PersonalID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "Tel_4",
                schema: "DMT",
                table: "MergeContactResult");
        }
    }
}
