using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePaymentUnknownPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositDetail_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropTable(
                name: "UnknownPaymentReverseDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "UnknownPaymentReverse",
                schema: "FIN");

            migrationBuilder.AddColumn<string>(
                name: "SAPRemark",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnknowPaymentCode",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "PaymentUnknownPayment",
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
                    UnknownPaymentID = table.Column<Guid>(nullable: true),
                    CancelRemark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentUnknownPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentUnknownPayment_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentUnknownPayment_UnknownPayment_UnknownPaymentID",
                        column: x => x.UnknownPaymentID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentUnknownPayment_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentUnknownPayment_CreatedByUserID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentUnknownPayment_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                column: "UnknownPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentUnknownPayment_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositDetail_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositDetail_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropTable(
                name: "PaymentUnknownPayment",
                schema: "FIN");

            migrationBuilder.DropColumn(
                name: "SAPRemark",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "UnknowPaymentCode",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UnknownPaymentReverse",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    CancelRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ReverseDate = table.Column<DateTime>(nullable: false),
                    UnknownPaymentID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownPaymentReverse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_UnknownPayment_UnknownPaymentID",
                        column: x => x.UnknownPaymentID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnknownPaymentReverseDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    UnknownPaymentReverseID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownPaymentReverseDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_UnknownPaymentReverse_UnknownPaymentReverseID",
                        column: x => x.UnknownPaymentReverseID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPaymentReverse",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_BookingID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_UnknownPaymentID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "UnknownPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_UnknownPaymentReverseID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "UnknownPaymentReverseID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositDetail_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "DepositDetail",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
