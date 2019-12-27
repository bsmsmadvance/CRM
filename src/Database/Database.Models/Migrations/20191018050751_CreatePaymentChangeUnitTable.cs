using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePaymentChangeUnitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeToBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentChangeUnit",
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
                    FromPaymentMethodID = table.Column<Guid>(nullable: true),
                    BasePaymentMethodID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentChangeUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentChangeUnit_PaymentMethod_BasePaymentMethodID",
                        column: x => x.BasePaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentChangeUnit_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentChangeUnit_PaymentMethod_FromPaymentMethodID",
                        column: x => x.FromPaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentChangeUnit_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "ChangeUnitWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                unique: true,
                filter: "[ChangeFromBookingID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment",
                column: "ChangeUnitWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentChangeUnit_BasePaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "BasePaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentChangeUnit_CreatedByUserID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentChangeUnit_FromPaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "FromPaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentChangeUnit_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment",
                column: "ChangeUnitWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeUnitWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToBookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "ChangeUnitWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeUnitWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropTable(
                name: "PaymentChangeUnit",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Payment_ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ChangeUnitWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "ChangeUnitWorkflowID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToBookingID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
