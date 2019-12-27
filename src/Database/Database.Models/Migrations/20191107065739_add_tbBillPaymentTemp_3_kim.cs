using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_tbBillPaymentTemp_3_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillPaymentTemp",
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
                    BatchID = table.Column<string>(maxLength: 50, nullable: true),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    ImportFileName = table.Column<string>(maxLength: 100, nullable: true),
                    BillPaymentImportTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    TotalRecord = table.Column<int>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPaymentTemp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillPaymentTemp_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "MST",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTemp_MasterCenter_BillPaymentImportTypeMasterCenterID",
                        column: x => x.BillPaymentImportTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTemp_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTemp_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPaymentTransactionTemp",
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
                    BillPaymentHeaderID = table.Column<Guid>(nullable: false),
                    DetailBatchID = table.Column<string>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    BankRef1 = table.Column<string>(maxLength: 50, nullable: true),
                    BankRef2 = table.Column<string>(maxLength: 50, nullable: true),
                    BankRef3 = table.Column<string>(maxLength: 50, nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    PayType = table.Column<string>(maxLength: 50, nullable: true),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    BillPaymentStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    ReconcileDate = table.Column<DateTime>(nullable: true),
                    BillPaymentDeleteReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPaymentTransactionTemp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                        column: x => x.BillPaymentDeleteReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_BillPayment_BillPaymentHeaderID",
                        column: x => x.BillPaymentHeaderID,
                        principalSchema: "FIN",
                        principalTable: "BillPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentStatusMasterCenterID",
                        column: x => x.BillPaymentStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransactionTemp_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTemp_BankAccountID",
                schema: "FIN",
                table: "BillPaymentTemp",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTemp_BillPaymentImportTypeMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTemp",
                column: "BillPaymentImportTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTemp_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTemp",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTemp_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTemp",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_BookingID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransactionTemp_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPaymentTemp",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "BillPaymentTransactionTemp",
                schema: "FIN");
        }
    }
}
