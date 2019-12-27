using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateReceiptHeaderAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarLockHistory",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "FIN");

            migrationBuilder.AddColumn<decimal>(
                name: "NextInstallmentAmount",
                schema: "FIN",
                table: "ReceiptHeader",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NextInstallmentPeriod",
                schema: "FIN",
                table: "ReceiptHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReceiptDetail",
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
                    ReceiptHeaderID = table.Column<Guid>(nullable: false),
                    PaymentItemID = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    DescriptionEN = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_PaymentItem_PaymentItemID",
                        column: x => x.PaymentItemID,
                        principalSchema: "FIN",
                        principalTable: "PaymentItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_ReceiptHeader_ReceiptHeaderID",
                        column: x => x.ReceiptHeaderID,
                        principalSchema: "FIN",
                        principalTable: "ReceiptHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_PaymentItemID",
                schema: "FIN",
                table: "ReceiptDetail",
                column: "PaymentItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptDetail",
                column: "ReceiptHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptDetail",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptDetail",
                schema: "FIN");

            migrationBuilder.DropColumn(
                name: "NextInstallmentAmount",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "NextInstallmentPeriod",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.CreateTable(
                name: "CalendarLockHistory",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CalendarLockID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarLockHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalendarLockHistory_CalendarLock_CalendarLockID",
                        column: x => x.CalendarLockID,
                        principalSchema: "ACC",
                        principalTable: "CalendarLock",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarLockHistory_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarLockHistory_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CompanyID = table.Column<Guid>(nullable: true),
                    ContactID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    PaymentID = table.Column<Guid>(nullable: true),
                    ReceiptNo = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receipt_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_CalendarLockID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "CalendarLockID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CompanyID",
                schema: "FIN",
                table: "Receipt",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_ContactID",
                schema: "FIN",
                table: "Receipt",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CreatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_PaymentID",
                schema: "FIN",
                table: "Receipt",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_UpdatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "UpdatedByUserID");
        }
    }
}
