using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeMasterCenterGroupKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenter_MasterCenterGroup_MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterCenterGroup",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenter_MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "ID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "MasterCenterGroup",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterCenterGroup",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenter_MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter",
                column: "MasterCenterGroupKey");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenter_MasterCenterGroup_MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter",
                column: "MasterCenterGroupKey",
                principalSchema: "MST",
                principalTable: "MasterCenterGroup",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenter_MasterCenterGroup_MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterCenterGroup",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenter_MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "MasterCenterGroupKey",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterCenterGroup",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenter_MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter",
                column: "MasterCenterGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenter_MasterCenterGroup_MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter",
                column: "MasterCenterGroupID",
                principalSchema: "MST",
                principalTable: "MasterCenterGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
