using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_in_TfAgOwner_kim_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAgreementOwnerID",
                schema: "SAL",
                table: "AgreementTransferOwner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FromAgreementOwnerID",
                schema: "SAL",
                table: "AgreementTransferOwner",
                nullable: true);
        }
    }
}
