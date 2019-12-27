using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class DropJuristicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Juristic",
                schema: "MST");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Juristic",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankAccountNo = table.Column<string>(maxLength: 10, nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameEN = table.Column<string>(maxLength: 100, nullable: true),
                    NameTH = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juristic", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Juristic_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Juristic_BankID",
                schema: "MST",
                table: "Juristic",
                column: "BankID");
        }
    }
}
