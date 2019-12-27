using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_CompanyProjectID_ReceiptTempHeader_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempHeader_Company_CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempHeader_Project_ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempHeader_Company_CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempHeader_Project_ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTempHeader_CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTempHeader_ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "FIN",
                table: "ReceiptTempHeader");
        }
    }
}
