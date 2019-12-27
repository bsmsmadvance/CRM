using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateDepositHeaderAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_Deposit_DepositID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Deposit",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_DepositID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "DepositID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.CreateTable(
                name: "DepositHeader",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    DepositNo = table.Column<string>(maxLength: 100, nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: false),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    TotalFeeAmount = table.Column<decimal>(type: "Money", nullable: false),
                    TotalRecord = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepositHeader_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "MST",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    DepositHeaderID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    FeeAmount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepositDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositDetail_DepositHeader_DepositHeaderID",
                        column: x => x.DepositHeaderID,
                        principalSchema: "FIN",
                        principalTable: "DepositHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositDetail_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositDetail_CreatedByUserID",
                schema: "FIN",
                table: "DepositDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositDetail_DepositHeaderID",
                schema: "FIN",
                table: "DepositDetail",
                column: "DepositHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositDetail_PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DepositDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositHeader_BankAccountID",
                schema: "FIN",
                table: "DepositHeader",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositHeader_CreatedByUserID",
                schema: "FIN",
                table: "DepositHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositHeader_UpdatedByUserID",
                schema: "FIN",
                table: "DepositHeader",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DepositHeader",
                schema: "FIN");

            migrationBuilder.AddColumn<Guid>(
                name: "DepositID",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deposit",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: false),
                    DepositNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deposit_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "MST",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposit_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposit_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_DepositID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "DepositID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_BankAccountID",
                schema: "FIN",
                table: "Deposit",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_CreatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_UpdatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_Deposit_DepositID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "DepositID",
                principalSchema: "FIN",
                principalTable: "Deposit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
