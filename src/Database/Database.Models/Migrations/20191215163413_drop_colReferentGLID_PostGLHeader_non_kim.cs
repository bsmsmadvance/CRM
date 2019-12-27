using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class drop_colReferentGLID_PostGLHeader_non_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDetail_MasterCenter_TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLHeader_PostGLHeader_ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropIndex(
                name: "IX_PostGLHeader_ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropIndex(
                name: "IX_PostGLDetail_TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.RenameColumn(
                name: "ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "BatchTypeMasterCenterID");

            migrationBuilder.AddColumn<string>(
                name: "TaxCode",
                schema: "MST",
                table: "BankAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxCode",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "BatchTypeMasterCenterID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "ReferentGLID");

            migrationBuilder.AddColumn<Guid>(
                name: "TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "ReferentGLID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "TaxCodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDetail_MasterCenter_TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "TaxCodeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLHeader_PostGLHeader_ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "ReferentGLID",
                principalSchema: "ACC",
                principalTable: "PostGLHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
