using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_Referent_PostGLHeader_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentDebitCard",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferentID",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferentType",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "ReferentID",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "ReferentType",
                schema: "ACC",
                table: "PostGLHeader");
        }
    }
}
