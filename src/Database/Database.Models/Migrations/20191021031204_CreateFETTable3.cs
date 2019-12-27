using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateFETTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FET",
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
                    FETRequesterMasterCenterID = table.Column<Guid>(nullable: true),
                    PaymentForeignBankTransferID = table.Column<Guid>(nullable: true),
                    PaymentCreditCardID = table.Column<Guid>(nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    FETStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    AttachFile = table.Column<string>(maxLength: 1000, nullable: true),
                    CancelRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    RejectByUserID = table.Column<Guid>(nullable: true),
                    RejectDate = table.Column<DateTime>(nullable: true),
                    RejectRemark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FET", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FET_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_MasterCenter_FETRequesterMasterCenterID",
                        column: x => x.FETRequesterMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_MasterCenter_FETStatusMasterCenterID",
                        column: x => x.FETStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_PaymentCreditCard_PaymentCreditCardID",
                        column: x => x.PaymentCreditCardID,
                        principalSchema: "FIN",
                        principalTable: "PaymentCreditCard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_PaymentForeignBankTransfer_PaymentForeignBankTransferID",
                        column: x => x.PaymentForeignBankTransferID,
                        principalSchema: "FIN",
                        principalTable: "PaymentForeignBankTransfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FET_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FET_BankID",
                schema: "FIN",
                table: "FET",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_BookingID",
                schema: "FIN",
                table: "FET",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_CreatedByUserID",
                schema: "FIN",
                table: "FET",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_FETRequesterMasterCenterID",
                schema: "FIN",
                table: "FET",
                column: "FETRequesterMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_FETStatusMasterCenterID",
                schema: "FIN",
                table: "FET",
                column: "FETStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_PaymentCreditCardID",
                schema: "FIN",
                table: "FET",
                column: "PaymentCreditCardID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET",
                column: "PaymentForeignBankTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_UpdatedByUserID",
                schema: "FIN",
                table: "FET",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FET",
                schema: "FIN");
        }
    }
}
