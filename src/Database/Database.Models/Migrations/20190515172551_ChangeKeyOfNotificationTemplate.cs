using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeKeyOfNotificationTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplate",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "ID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "NTF",
                table: "NotificationTemplate",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplate",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplate",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "NTF",
                table: "NotificationTemplate",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                schema: "NTF",
                table: "NotificationTemplate",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplate",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "ID");
        }
    }
}
