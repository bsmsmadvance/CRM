using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeErrorMessageToResponseMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ErrorMessage",
                schema: "USR",
                table: "UserBackgroundJob",
                newName: "ResponseMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponseMessage",
                schema: "USR",
                table: "UserBackgroundJob",
                newName: "ErrorMessage");
        }
    }
}
