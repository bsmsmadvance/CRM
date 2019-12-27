using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveIDFromErrorMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorMessage",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropColumn(
                name: "ID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "ErrorMessage",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorMessage",
                schema: "MST",
                table: "ErrorMessage",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorMessage",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "ErrorMessage",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                schema: "MST",
                table: "ErrorMessage",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorMessage",
                schema: "MST",
                table: "ErrorMessage",
                column: "ID");
        }
    }
}
