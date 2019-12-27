using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddNewTelAndEmailToMatchMergeResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewEmailID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewTel1ID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewTel2ID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewTel3ID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewTel4ID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewEmailID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "NewTel1ID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "NewTel2ID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "NewTel3ID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "NewTel4ID",
                schema: "DMT",
                table: "MergeContactResult");
        }
    }
}
