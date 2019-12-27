using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateTransferTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_LCID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_BankBranch_BankBranchID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_Bank_BankID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_BankBranch_BankBranchID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Contact_ContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropTable(
                name: "MortgageWithBank",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_ContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferCash_BankBranchID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "ContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "PayTo",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "BankBranchID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "PayDate",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "PayTo",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "FatherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MarriageNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MotherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FatherNational",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MarriageNational",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MotherNational",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "TransferAmount",
                schema: "SAL",
                table: "TransferCheque",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "PayToCompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_PayToCompanyID");

            migrationBuilder.RenameColumn(
                name: "TransferAmount",
                schema: "SAL",
                table: "TransferCash",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "CashPayToMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_CashPayToMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "TransferSaleUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_TransferSaleUserID");

            migrationBuilder.AddColumn<string>(
                name: "AuthorityName",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactFirstName",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactLastname",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeignDistrict",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeignProvince",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeignSubDistrict",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromContactID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNoTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssignAuthority",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssignAuthorityByCompany",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastNameTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageName",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MooTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryEN",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictEN",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceEN",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictEN",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleExtTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VillageTH",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayDate",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongCompany",
                schema: "SAL",
                table: "TransferCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TransferNo",
                schema: "SAL",
                table: "Transfer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "APBalance",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "APBalanceTransfer",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "APChangeAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "APChangeAmountBeforeTransfer",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BusinessTax",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CompanyIncomeTax",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CompanyPayFee",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CopyDocumentAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerPayFee",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerPayMortgage",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DocumentFee",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreeFee",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GoTollWayAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GoTransportAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAPBalanceTransfer",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAPGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAPPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegalEntityBalanceTransfer",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegalEntityGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegalEntityPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "LandArea",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandEstimatePrice",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandOfficeTollWayAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandOfficeTransportAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LegalEntityBalance",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LegalEntityBalanceTransfer",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LocalTax",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinistryCashOrCheque",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinistryPCard",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PettyCashAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnTollWayAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnTransportAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StandardArea",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SupportOfficerAmount",
                schema: "SAL",
                table: "Transfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageOtherNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageName",
                schema: "CTM",
                table: "Contact",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditBanking",
                schema: "SAL",
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
                    BookingID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    FinancialInstitutionMasterCenterID = table.Column<Guid>(nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    BankBranchID = table.Column<Guid>(nullable: true),
                    OtherBank = table.Column<string>(maxLength: 1000, nullable: true),
                    LoanSubmitDate = table.Column<DateTime>(nullable: true),
                    LoanAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedLoanAPAmount = table.Column<decimal>(type: "Money", nullable: false),
                    InsuranceAmount = table.Column<decimal>(type: "Money", nullable: false),
                    InsuranceOnFireAmount = table.Column<decimal>(type: "Money", nullable: false),
                    FirstDeductAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ReturnCustomerAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LoanStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    ResultDate = table.Column<DateTime>(nullable: true),
                    IsUseBank = table.Column<bool>(nullable: true),
                    BankReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    UseBankReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    UseBankOtherReason = table.Column<string>(maxLength: 5000, nullable: true),
                    NotUseBankReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    NotUseBankOtherReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    NotUseBankOtherReason = table.Column<string>(maxLength: 5000, nullable: true),
                    BankRejectReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    BankWaitingReasonMasterCenterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditBanking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CreditBanking_BankBranch_BankBranchID",
                        column: x => x.BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_BankReasonMasterCenterID",
                        column: x => x.BankReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_BankRejectReasonMasterCenterID",
                        column: x => x.BankRejectReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_BankWaitingReasonMasterCenterID",
                        column: x => x.BankWaitingReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_FinancialInstitutionMasterCenterID",
                        column: x => x.FinancialInstitutionMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_LoanStatusMasterCenterID",
                        column: x => x.LoanStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_NotUseBankOtherReasonMasterCenterID",
                        column: x => x.NotUseBankOtherReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBanking_MasterCenter_UseBankReasonMasterCenterID",
                        column: x => x.UseBankReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditBankingPrintingHistory",
                schema: "SAL",
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
                    BookingID = table.Column<Guid>(nullable: true),
                    IsSelectAll = table.Column<bool>(nullable: false),
                    IsPersonal = table.Column<bool>(nullable: false),
                    IsCompany = table.Column<bool>(nullable: false),
                    IsPartnership = table.Column<bool>(nullable: false),
                    IsRegisteredStore = table.Column<bool>(nullable: false),
                    IsNotRegisteredStore = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditBankingPrintingHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CreditBankingPrintingHistory_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBankingPrintingHistory_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditBankingPrintingHistory_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferBankTransfer",
                schema: "SAL",
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
                    TransferID = table.Column<Guid>(nullable: false),
                    BankTransferPayToMasterCenterID = table.Column<Guid>(nullable: true),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: true),
                    IsWrongTransferDate = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferBankTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferBankTransfer_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "MST",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferBankTransfer_MasterCenter_BankTransferPayToMasterCenterID",
                        column: x => x.BankTransferPayToMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferBankTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferBankTransfer_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferBankTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactTitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_CountryID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_DistrictID",
                schema: "SAL",
                table: "TransferOwner",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_FromContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "FromContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "NationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_ProvinceID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_SubDistrictID",
                schema: "SAL",
                table: "TransferOwner",
                column: "SubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque",
                column: "ChequePayToMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer",
                column: "MeterChequeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ProjectID",
                schema: "SAL",
                table: "Transfer",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "CreditBankingTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "FatherNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "MarriageNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "MotherNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "FatherNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "MarriageNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "MotherNationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BankBranchID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BankID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BankReasonMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BankReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BankRejectReasonMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BankRejectReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BankWaitingReasonMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BankWaitingReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_BookingID",
                schema: "SAL",
                table: "CreditBanking",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_CreatedByUserID",
                schema: "SAL",
                table: "CreditBanking",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_FinancialInstitutionMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "FinancialInstitutionMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_LoanStatusMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "LoanStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_NotUseBankOtherReasonMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "NotUseBankOtherReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_UpdatedByUserID",
                schema: "SAL",
                table: "CreditBanking",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBanking_UseBankReasonMasterCenterID",
                schema: "SAL",
                table: "CreditBanking",
                column: "UseBankReasonMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBankingPrintingHistory_BookingID",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBankingPrintingHistory_CreatedByUserID",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBankingPrintingHistory_UpdatedByUserID",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBankTransfer_BankAccountID",
                schema: "SAL",
                table: "TransferBankTransfer",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBankTransfer_BankTransferPayToMasterCenterID",
                schema: "SAL",
                table: "TransferBankTransfer",
                column: "BankTransferPayToMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBankTransfer_CreatedByUserID",
                schema: "SAL",
                table: "TransferBankTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBankTransfer_TransferID",
                schema: "SAL",
                table: "TransferBankTransfer",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBankTransfer_UpdatedByUserID",
                schema: "SAL",
                table: "TransferBankTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "FatherNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "MarriageNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "MotherNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "FatherNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "MarriageNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "MotherNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "CreditBankingTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_MasterCenter_MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer",
                column: "MeterChequeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Project_ProjectID",
                schema: "SAL",
                table: "Transfer",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_TransferSaleUserID",
                schema: "SAL",
                table: "Transfer",
                column: "TransferSaleUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_MasterCenter_CashPayToMasterCenterID",
                schema: "SAL",
                table: "TransferCash",
                column: "CashPayToMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_MasterCenter_ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque",
                column: "ChequePayToMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_Company_PayToCompanyID",
                schema: "SAL",
                table: "TransferCheque",
                column: "PayToCompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactTitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Country_CountryID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CountryID",
                principalSchema: "MST",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_District_DistrictID",
                schema: "SAL",
                table: "TransferOwner",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Contact_FromContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "FromContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageNationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "NationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Province_ProvinceID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_SubDistrict_SubDistrictID",
                schema: "SAL",
                table: "TransferOwner",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_MasterCenter_MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Project_ProjectID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_TransferSaleUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_MasterCenter_CashPayToMasterCenterID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_MasterCenter_ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_Company_PayToCompanyID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Country_CountryID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_District_DistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Contact_FromContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Province_ProvinceID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_SubDistrict_SubDistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropTable(
                name: "CreditBanking",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "CreditBankingPrintingHistory",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferBankTransfer",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_CountryID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_DistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_FromContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_ProvinceID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_SubDistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferCheque_ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_ProjectID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_Contact_FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AuthorityName",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ContactFirstName",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ContactLastname",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "CountryID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "DistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "FirstNameTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ForeignDistrict",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ForeignProvince",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ForeignSubDistrict",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "FromContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "HouseNoTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "IsAssignAuthority",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "IsAssignAuthorityByCompany",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "LastNameTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MarriageName",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MooTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "NationalMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherCountryEN",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherCountryTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherDistrictEN",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherProvinceEN",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherProvinceTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictEN",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ParentName",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "RoadTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "SoiTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "TitleExtTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "VillageTH",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "ChequePayToMasterCenterID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "IsWrongCompany",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "APBalance",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "APBalanceTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "APChangeAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "APChangeAmountBeforeTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "BusinessTax",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CompanyIncomeTax",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CompanyPayFee",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CopyDocumentAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CustomerPayFee",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CustomerPayMortgage",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "DocumentFee",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "FreeFee",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "GoTollWayAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "GoTransportAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsAPBalanceTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsAPGiveChange",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsAPPayWithMemo",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsLegalEntityBalanceTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsLegalEntityGiveChange",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsLegalEntityPayWithMemo",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LandArea",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LandEstimatePrice",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LandOfficeTollWayAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LandOfficeTransportAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LegalEntityBalance",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LegalEntityBalanceTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LocalTax",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "MeterChequeMasterCenterID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "MinistryCashOrCheque",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "MinistryPCard",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "PettyCashAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ReturnTollWayAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ReturnTransportAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "StandardArea",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SupportOfficerAmount",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CreditBankingTypeMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "FatherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MarriageNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MotherNationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FatherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MarriageNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MotherNationalMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "PayToCompanyID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "BankBranchID");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "SAL",
                table: "TransferCheque",
                newName: "TransferAmount");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_PayToCompanyID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "CashPayToMasterCenterID",
                schema: "SAL",
                table: "TransferCash",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "SAL",
                table: "TransferCash",
                newName: "TransferAmount");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_CashPayToMasterCenterID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_BankID");

            migrationBuilder.RenameColumn(
                name: "TransferSaleUserID",
                schema: "SAL",
                table: "Transfer",
                newName: "LCID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_TransferSaleUserID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_LCID");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PayDate",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayTo",
                schema: "SAL",
                table: "TransferCheque",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayDate",
                schema: "SAL",
                table: "TransferCash",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayTo",
                schema: "SAL",
                table: "TransferCash",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransferNo",
                schema: "SAL",
                table: "Transfer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageOtherNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarriageName",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MortgageWithBank",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedLoadAPAmount = table.Column<decimal>(type: "Money", nullable: false),
                    BankID = table.Column<Guid>(nullable: true),
                    ChoseBankOtherReason = table.Column<string>(nullable: true),
                    ChosenBankReason = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    FirstDeductAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Interest = table.Column<decimal>(type: "Money", nullable: false),
                    InterestOnFire = table.Column<decimal>(type: "Money", nullable: false),
                    IsChosenBankStatus = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LoanAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LoanStatus = table.Column<string>(nullable: true),
                    LoanSubmitDate = table.Column<DateTime>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    ReturnCustomerAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MortgageWithBank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MortgageWithBank_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MortgageWithBank_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MortgageWithBank_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                column: "BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageWithBank_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageWithBank_CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageWithBank_UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_LCID",
                schema: "SAL",
                table: "Transfer",
                column: "LCID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_BankBranch_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_Bank_BankID",
                schema: "SAL",
                table: "TransferCash",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_BankBranch_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Contact_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
