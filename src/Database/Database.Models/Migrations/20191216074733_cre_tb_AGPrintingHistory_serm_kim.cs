using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class cre_tb_AGPrintingHistory_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementPrintingHistory",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    AgreementID = table.Column<Guid>(nullable: true),
                    AgreementPrintingDate = table.Column<DateTime>(nullable: true),
                    AgreementPrintingByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementPrintingHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementPrintingHistory_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementPrintingHistory_User_AgreementPrintingByUserID",
                        column: x => x.AgreementPrintingByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementPrintingHistory_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementPrintingHistory_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementPrintingHistory_AgreementID",
                schema: "SAL",
                table: "AgreementPrintingHistory",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementPrintingHistory_AgreementPrintingByUserID",
                schema: "SAL",
                table: "AgreementPrintingHistory",
                column: "AgreementPrintingByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementPrintingHistory_CreatedByUserID",
                schema: "SAL",
                table: "AgreementPrintingHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementPrintingHistory_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementPrintingHistory",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementPrintingHistory",
                schema: "SAL");
        }
    }
}
