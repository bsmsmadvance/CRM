using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyTitleDeedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_AddressID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_Address_AddressID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "AddressID",
                principalSchema: "PRJ",
                principalTable: "Address",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_Address_AddressID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_AddressID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AddressID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);
        }
    }
}
