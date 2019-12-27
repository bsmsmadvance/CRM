using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class SetNullableMasterProInPromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Transfer_TransferID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_Contact_ContactID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "BookingCustomer",
                newName: "NationalMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingCustomer_ContactID",
                schema: "SAL",
                table: "BookingCustomer",
                newName: "IX_BookingCustomer_NationalMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_BookingID");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CitizenExpireDate",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactFirstName",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactLastname",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherOtherNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEN",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameTH",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromContactID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsThaiNationality",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVIP",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEN",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameTH",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineID",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageName",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameEN",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherOtherNational",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpportunityCount",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberExt",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxID",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleExtEN",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleExtTH",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeChatID",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppID",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleENMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_FromContactID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "FromContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "GenderMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "LastOpportunityID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_Booking_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleENMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_Contact_FromContactID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "FromContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_MasterCenter_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "GenderMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_Opportunity_LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "LastOpportunityID",
                principalSchema: "CTM",
                principalTable: "Opportunity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "NationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Booking_BookingID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_Contact_FromContactID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_MasterCenter_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_Opportunity_LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_FromContactID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "CitizenExpireDate",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactFirstName",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactLastname",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FatherName",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FatherNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FatherOtherNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FirstNameEN",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FirstNameTH",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "FromContactID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "IsThaiNationality",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "IsVIP",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "LastNameEN",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "LastNameTH",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "LineID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MarriageName",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MarriageNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MiddleNameEN",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MotherName",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MotherNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "MotherOtherNational",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "Nickname",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "OpportunityCount",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "PhoneNumberExt",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "TaxID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "TitleExtEN",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "TitleExtTH",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "WeChatID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "WhatsAppID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.RenameColumn(
                name: "NationalMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingCustomer_NationalMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                newName: "IX_BookingCustomer_ContactID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_TransferID");

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_Transfer_TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_Contact_ContactID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
