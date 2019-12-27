using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeWeChatIDName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineId",
                schema: "PRJ",
                table: "Project",
                newName: "LineID");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                schema: "PRJ",
                table: "Project",
                newName: "WhatsAppID");

            migrationBuilder.RenameColumn(
                name: "WeChat",
                schema: "PRJ",
                table: "Project",
                newName: "WeChatID");

            migrationBuilder.AddColumn<string>(
                name: "GLPreTransferBatchID",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForeignUnit",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GLPreTransferBatchID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsForeignUnit",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "LineID",
                schema: "PRJ",
                table: "Project",
                newName: "LineId");

            migrationBuilder.RenameColumn(
                name: "WhatsAppID",
                schema: "PRJ",
                table: "Project",
                newName: "WhatsApp");

            migrationBuilder.RenameColumn(
                name: "WeChatID",
                schema: "PRJ",
                table: "Project",
                newName: "WeChat");
        }
    }
}
