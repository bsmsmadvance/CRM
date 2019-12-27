using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_AgencyID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "BookingOwner",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "UnitPriceInstallmentItem",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AgencyID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ActiveDate",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "BookingPaymentStatus",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingType",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "SignBookedDate",
                schema: "SAL",
                table: "Booking",
                newName: "ContractDueDate");

            migrationBuilder.RenameColumn(
                name: "AgencyID",
                schema: "SAL",
                table: "Booking",
                newName: "SaleUserID");

            migrationBuilder.RenameColumn(
                name: "ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking",
                newName: "ProjectSaleUserID");

            migrationBuilder.AddColumn<Guid>(
                name: "FromMasterPriceListItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentOfUnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialInstallment",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "SAL",
                table: "UnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "BookingNo",
                schema: "SAL",
                table: "Booking",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CancelByUserID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                schema: "SAL",
                table: "Booking",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CancelType",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeUnitDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromChangeUnit",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaleArea",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingCustomer",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    ContactID = table.Column<Guid>(nullable: true),
                    IsMainCustomer = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCustomer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AgentID",
                schema: "SAL",
                table: "Booking",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingCancelReturnMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingReasonReturnMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CancelByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CancelByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeUnitByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                column: "IntroducerContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ModelID",
                schema: "SAL",
                table: "Booking",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ProjectID",
                schema: "SAL",
                table: "Booking",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "PreferStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_BookingID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_MasterCenter_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "PreferStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AgentEmplyee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID",
                principalSchema: "MST",
                principalTable: "AgentEmplyee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Agent_AgentID",
                schema: "SAL",
                table: "Booking",
                column: "AgentID",
                principalSchema: "MST",
                principalTable: "Agent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingCancelReturnMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingReasonReturnMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_CancelByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CancelByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeUnitByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                column: "IntroducerContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Model_ModelID",
                schema: "SAL",
                table: "Booking",
                column: "ModelID",
                principalSchema: "PRJ",
                principalTable: "Model",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Project_ProjectID",
                schema: "SAL",
                table: "Booking",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_MasterCenter_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AgentEmplyee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Agent_AgentID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_CancelByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeToUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_IntroducerContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Model_ModelID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Project_ProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "BookingCustomer",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AgentID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CancelByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeToUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_IntroducerContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ModelID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "FromMasterPriceListItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "InstallmentOfUnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsSpecialInstallment",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "AgentID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingStatusMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelRemark",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelType",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ChangeFromUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ChangeToUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ChangeUnitByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ChangeUnitDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IntroducerContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsFromChangeUnit",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ModelID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SaleArea",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PreferStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "SaleUserID",
                schema: "SAL",
                table: "Booking",
                newName: "AgencyID");

            migrationBuilder.RenameColumn(
                name: "ProjectSaleUserID",
                schema: "SAL",
                table: "Booking",
                newName: "ActiveUnitPriceID");

            migrationBuilder.RenameColumn(
                name: "ContractDueDate",
                schema: "SAL",
                table: "Booking",
                newName: "SignBookedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                schema: "SAL",
                table: "UnitPrice",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookingNo",
                schema: "SAL",
                table: "Booking",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingPaymentStatus",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingType",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactID",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookingOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    ContactID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitPriceInstallmentItem",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    Period = table.Column<int>(nullable: false),
                    UnitPriceItemID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPriceInstallmentItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitPriceInstallmentItem_UnitPriceItem_UnitPriceItemID",
                        column: x => x.UnitPriceItemID,
                        principalSchema: "SAL",
                        principalTable: "UnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking",
                column: "ActiveUnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AgencyID",
                schema: "SAL",
                table: "Booking",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ContactID",
                schema: "SAL",
                table: "Booking",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceInstallmentItem_UnitPriceItemID",
                schema: "SAL",
                table: "UnitPriceInstallmentItem",
                column: "UnitPriceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking",
                column: "ActiveUnitPriceID",
                principalSchema: "SAL",
                principalTable: "UnitPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_AgencyID",
                schema: "SAL",
                table: "Booking",
                column: "AgencyID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_ContactID",
                schema: "SAL",
                table: "Booking",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
