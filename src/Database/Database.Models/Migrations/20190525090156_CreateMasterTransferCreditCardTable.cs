using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateMasterTransferCreditCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterTransferCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterTransferPromotionID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: true),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    UnitTH = table.Column<string>(nullable: true),
                    UnitEN = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferCreditCardItem_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTransferCreditCardItem_MasterTransferPromotion_MasterTransferPromotionID",
                        column: x => x.MasterTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_BankID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_MasterTransferPromotionID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "MasterTransferPromotionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterTransferCreditCardItem",
                schema: "PRM");
        }
    }
}
