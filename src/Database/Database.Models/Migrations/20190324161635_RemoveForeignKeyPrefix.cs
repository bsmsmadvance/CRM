using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveForeignKeyPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankBranch_MST_BankBranchID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_MST_BankID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_District_MST_DistrictID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_Bank_MST_Bank",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_BankBranch_MST_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_Bank_MST_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_District_MST_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_Project_PRJ_ProjectID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_User_USR_LCUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_USR_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_USR_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_User_USR_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddressProject_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Bank_MST_BankID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_Contact_CTM_ContactID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_Project_PRJ_ProjectID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposit_BankAccount_ACC_BankAccount",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_SAL_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_Company_MST_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDetail_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_Province_MST_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_Bank_MST_BankID",
                schema: "FIN",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_Project_PRJ_ProjectID",
                schema: "FIN",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Booking_SAL_BookingID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBankTransfer_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_Company_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_Company_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentQRCode_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Contact_CTM_ContactID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Company_MST_CompanyID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_CTM_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_Contact_CTM_ContactID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_Company_MST_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_Bank_MST_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_DownPaymentLetter_Agreement_SAL_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferLetter_Agreement_SAL_AgreementID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileInstallation_User_USR_UserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileNotification_User_USR_UserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_WebNotification_User_USR_UserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStory_Contact_CTM_ContactID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_Booking_SAL_BookingID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Project_PRJ_ProjectID",
                schema: "PRM",
                table: "Promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionPreSale_Project_PRJ_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_Quotation_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_Quotation_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Booking_SAL_BookingID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Contact_CTM_ContactID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementDownPeriod_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_CTM_ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_AgencyID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingOwner_Booking_SAL_BookingID",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_MortgageWithBank_Bank_MST_BankID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationCompare_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceive_TitledeedDetail_PRJ_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceiveHistory_User_USR_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_USR_LCID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_BankBranch_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_Bank_MST_BankID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_BankBranch_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_Bank_MST_BankID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferDocument_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Unit_PRJ_NewUnitID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Unit_PRJ_OldUnitID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenu_Menu_MST_MenuID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDefaultProject_Project_PRJ_ProjectID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_User_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_Role_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_User_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_Role_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.RenameColumn(
                name: "USR_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "ApproverID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApproverTemplate_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "IX_WorkflowApproverTemplate_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApproverTemplate_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "IX_WorkflowApproverTemplate_ApproverID");

            migrationBuilder.RenameColumn(
                name: "USR_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "ApproverID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApprover_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "IX_WorkflowApprover_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApprover_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "IX_WorkflowApprover_ApproverID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_UserDefaultProject_PRJ_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                newName: "IX_UserDefaultProject_ProjectID");

            migrationBuilder.RenameColumn(
                name: "MST_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                newName: "MenuID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteMenu_MST_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                newName: "IX_FavoriteMenu_MenuID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "OldUnitID");

            migrationBuilder.RenameColumn(
                name: "PRJ_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "NewUnitID");

            migrationBuilder.RenameColumn(
                name: "PRJ_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_SAL_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_OldUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_PRJ_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_NewUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_PRJ_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_AgreementID");

            migrationBuilder.RenameColumn(
                name: "SAL_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "TransferID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferOwner_SAL_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "IX_TransferOwner_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferOwner_CTM_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "IX_TransferOwner_ContactID");

            migrationBuilder.RenameColumn(
                name: "SAL_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                newName: "TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferDocument_SAL_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                newName: "IX_TransferDocument_TransferID");

            migrationBuilder.RenameColumn(
                name: "SAL_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "TransferID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_SAL_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_MST_BankID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "SAL_TransferID",
                schema: "SAL",
                table: "TransferCash",
                newName: "TransferID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_SAL_TransferID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_MST_BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "USR_LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "SAL",
                table: "Transfer",
                newName: "LCID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "SAL",
                table: "Transfer",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_USR_LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_SAL_AgreementID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_LCID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_PRJ_UnitID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_AgreementID");

            migrationBuilder.RenameColumn(
                name: "USR_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                newName: "ActorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_TitledeedReceiveHistory_USR_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                newName: "IX_TitledeedReceiveHistory_ActorUserID");

            migrationBuilder.RenameColumn(
                name: "PRJ_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                newName: "TitledeedDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_TitledeedReceive_PRJ_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                newName: "IX_TitledeedReceive_TitledeedDetailID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                newName: "UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationCompare_PRJ_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                newName: "IX_QuotationCompare_UnitID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "SAL",
                table: "Quotation",
                newName: "UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Quotation_PRJ_UnitID",
                schema: "SAL",
                table: "Quotation",
                newName: "IX_Quotation_UnitID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_MortgageWithBank_MST_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                newName: "IX_MortgageWithBank_BankID");

            migrationBuilder.RenameColumn(
                name: "SAL_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "ContactID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingOwner_SAL_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "IX_BookingOwner_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingOwner_CTM_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "IX_BookingOwner_BookingID");

            migrationBuilder.RenameColumn(
                name: "USR_SaleID",
                schema: "SAL",
                table: "Booking",
                newName: "SaleID");

            migrationBuilder.RenameColumn(
                name: "USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                newName: "SaleAtProjectID");

            migrationBuilder.RenameColumn(
                name: "USR_AgencyID",
                schema: "SAL",
                table: "Booking",
                newName: "AgencyID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "SAL",
                table: "Booking",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "SAL",
                table: "Booking",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_USR_SaleID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_SaleID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_SaleAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_USR_AgencyID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_AgencyID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PRJ_UnitID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CTM_ContactID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_ContactID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "ContactID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementOwner_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "IX_AgreementOwner_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementOwner_CTM_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "IX_AgreementOwner_AgreementID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementDownPeriod_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                newName: "IX_AgreementDownPeriod_AgreementID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "SAL",
                table: "Agreement",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_PRJ_UnitID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_CTM_ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_ContactID");

            migrationBuilder.RenameColumn(
                name: "SAL_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_SAL_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_BookingID");

            migrationBuilder.RenameColumn(
                name: "SAL_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                newName: "QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationTransferPromotion_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                newName: "IX_QuotationTransferPromotion_QuotationID");

            migrationBuilder.RenameColumn(
                name: "SAL_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationBookingPromotion_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "IX_QuotationBookingPromotion_QuotationID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionPreSale_PRJ_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                newName: "IX_PromotionPreSale_ProjectID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "PRM",
                table: "Promotion",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Promotion_PRJ_ProjectID",
                schema: "PRM",
                table: "Promotion",
                newName: "IX_Promotion_ProjectID");

            migrationBuilder.RenameColumn(
                name: "SAL_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotion_SAL_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "IX_BookingPromotion_BookingID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "OST",
                table: "ContactStory",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactStory_CTM_ContactID",
                schema: "OST",
                table: "ContactStory",
                newName: "IX_ContactStory_ContactID");

            migrationBuilder.RenameColumn(
                name: "USR_UserID",
                schema: "NTF",
                table: "WebNotification",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WebNotification_USR_UserID",
                schema: "NTF",
                table: "WebNotification",
                newName: "IX_WebNotification_UserID");

            migrationBuilder.RenameColumn(
                name: "USR_UserID",
                schema: "NTF",
                table: "MobileNotification",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MobileNotification_USR_UserID",
                schema: "NTF",
                table: "MobileNotification",
                newName: "IX_MobileNotification_UserID");

            migrationBuilder.RenameColumn(
                name: "USR_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MobileInstallation_USR_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                newName: "IX_MobileInstallation_UserID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferLetter_SAL_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                newName: "IX_TransferLetter_AgreementID");

            migrationBuilder.RenameColumn(
                name: "SAL_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_DownPaymentLetter_SAL_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "IX_DownPaymentLetter_AgreementID");

            migrationBuilder.RenameColumn(
                name: "MST_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "BankAccountID");

            migrationBuilder.RenameColumn(
                name: "ACC_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "AttachFileFromBankID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_MST_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_ACC_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_AttachFileFromBankID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "ContactID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTemp_MST_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "IX_ReceiptTemp_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTemp_CTM_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "IX_ReceiptTemp_CompanyID");

            migrationBuilder.RenameColumn(
                name: "CTM_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "SendToContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendEmailHistory_CTM_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IX_ReceiptSendEmailHistory_SendToContactID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "FIN",
                table: "Receipt",
                newName: "ContactID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "FIN",
                table: "Receipt",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_MST_CompanyID",
                schema: "FIN",
                table: "Receipt",
                newName: "IX_Receipt_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_CTM_ContactID",
                schema: "FIN",
                table: "Receipt",
                newName: "IX_Receipt_CompanyID");

            migrationBuilder.RenameColumn(
                name: "ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                newName: "BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentQRCode_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                newName: "IX_PaymentQRCode_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "PayToCompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_PayToCompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_MST_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentForeignBankTransfer_MST_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IX_PaymentForeignBankTransfer_BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCreditCard_MST_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                newName: "IX_PaymentCreditCard_BankID");

            migrationBuilder.RenameColumn(
                name: "MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "PayToCompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_PayToCompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_MST_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                newName: "BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentBankTransfer_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                newName: "IX_PaymentBankTransfer_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "SAL_BookingID",
                schema: "FIN",
                table: "Payment",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_SAL_BookingID",
                schema: "FIN",
                table: "Payment",
                newName: "IX_Payment_BookingID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "FIN",
                table: "EDC",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "EDC",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_EDC_PRJ_ProjectID",
                schema: "FIN",
                table: "EDC",
                newName: "IX_EDC_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_EDC_MST_BankID",
                schema: "FIN",
                table: "EDC",
                newName: "IX_EDC_BankID");

            migrationBuilder.RenameColumn(
                name: "MST_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "ProvinceID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_MST_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_ProvinceID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_MST_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_MST_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDetail_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                newName: "IX_DirectCreditDetail_BankID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExport_MST_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "IX_DirectCreditDebitExport_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExport_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "IX_DirectCreditDebitExport_BankID");

            migrationBuilder.RenameColumn(
                name: "SAL_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_SAL_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_BookingID");

            migrationBuilder.RenameColumn(
                name: "ACC_BankAccount",
                schema: "FIN",
                table: "Deposit",
                newName: "BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Deposit_ACC_BankAccount",
                schema: "FIN",
                table: "Deposit",
                newName: "IX_Deposit_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "CTM_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerWallet_PRJ_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "IX_CustomerWallet_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerWallet_CTM_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "IX_CustomerWallet_ContactID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_MST_BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_BankID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "Visitor",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_PRJ_ProjectID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_ProjectID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Opportunity_PRJ_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                newName: "IX_Opportunity_ProjectID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_ProjectID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddressProject_PRJ_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                newName: "IX_ContactAddressProject_ProjectID");

            migrationBuilder.RenameColumn(
                name: "USR_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "LCCTransferID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateTransfer_USR_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "IX_CalculateTransfer_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateTransfer_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "IX_CalculateTransfer_LCCTransferID");

            migrationBuilder.RenameColumn(
                name: "USR_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "LCClosedDealID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "LCAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_USR_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_LCClosedDealID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_LCAtProjectID");

            migrationBuilder.RenameColumn(
                name: "USR_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "LCClosedDealUserID");

            migrationBuilder.RenameColumn(
                name: "PRJ_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "LCAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_USR_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_LCClosedDealUserID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_PRJ_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_LCAtProjectID");

            migrationBuilder.RenameColumn(
                name: "USR_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "PRJ_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "LCUserID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateOther_USR_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "IX_CalculateOther_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateOther_PRJ_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "IX_CalculateOther_LCUserID");

            migrationBuilder.RenameColumn(
                name: "MST_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "DistrictID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_MST_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_DistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_MST_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_MST_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_MST_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_Bank",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLChartOfAccount_MST_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "IX_PostGLChartOfAccount_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLChartOfAccount_MST_Bank",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "IX_PostGLChartOfAccount_BankID");

            migrationBuilder.RenameColumn(
                name: "MST_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                newName: "DistrictID");

            migrationBuilder.RenameColumn(
                name: "MST_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "MST_BankID",
                schema: "ACC",
                table: "BankAccount",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "MST_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                newName: "BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_MST_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_DistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_MST_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_MST_BankID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_MST_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_BankBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankBranch_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_BankID",
                schema: "ACC",
                table: "BankAccount",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Company_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_District_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_Bank_BankID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_Company_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_BankBranch_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_Bank_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_Company_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_District_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_User_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "LCUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_Project_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "LCAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "LCClosedDealUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_Unit_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                column: "LCAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                column: "LCClosedDealID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_Unit_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_User_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "LCCTransferID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_Unit_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddressProject_Project_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Project_ProjectID",
                schema: "CTM",
                table: "Lead",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_Project_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_Project_ProjectID",
                schema: "CTM",
                table: "Visitor",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Bank_BankID",
                schema: "FIN",
                table: "BillPayment",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_Contact_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_Project_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposit_BankAccount_BankAccountID",
                schema: "FIN",
                table: "Deposit",
                column: "BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_Company_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDetail_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_BankBranch_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_Bank_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_Province_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_Bank_BankID",
                schema: "FIN",
                table: "EDC",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_Project_ProjectID",
                schema: "FIN",
                table: "EDC",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Booking_BookingID",
                schema: "FIN",
                table: "Payment",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_BankBranch_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_Bank_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_Company_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "PayToCompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_Bank_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_BankBranch_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_Bank_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_Company_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "PayToCompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentQRCode_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Company_CompanyID",
                schema: "FIN",
                table: "Receipt",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Contact_ContactID",
                schema: "FIN",
                table: "Receipt",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "SendToContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_Company_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_Contact_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_Bank_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "AttachFileFromBankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_BankAccount_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DownPaymentLetter_Agreement_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferLetter_Agreement_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileInstallation_User_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileNotification_User_UserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebNotification_User_UserID",
                schema: "NTF",
                table: "WebNotification",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStory_Contact_ContactID",
                schema: "OST",
                table: "ContactStory",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_Booking_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Project_ProjectID",
                schema: "PRM",
                table: "Promotion",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionPreSale_Project_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_Quotation_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_Quotation_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Agreement_Contact_ContactID",
                schema: "SAL",
                table: "Agreement",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Unit_UnitID",
                schema: "SAL",
                table: "Agreement",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementDownPeriod_Agreement_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Agreement_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Contact_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                column: "SaleAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_SaleID",
                schema: "SAL",
                table: "Booking",
                column: "SaleID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_UnitID",
                schema: "SAL",
                table: "Booking",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOwner_Booking_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOwner_Contact_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MortgageWithBank_Bank_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Unit_UnitID",
                schema: "SAL",
                table: "Quotation",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationCompare_Unit_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceive_TitledeedDetail_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "TitledeedDetailID",
                principalSchema: "PRJ",
                principalTable: "TitledeedDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceiveHistory_User_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "ActorUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Agreement_AgreementID",
                schema: "SAL",
                table: "Transfer",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Transfer_Unit_UnitID",
                schema: "SAL",
                table: "Transfer",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
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
                name: "FK_TransferCash_Transfer_TransferID",
                schema: "SAL",
                table: "TransferCash",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_TransferCheque_Bank_BankID",
                schema: "SAL",
                table: "TransferCheque",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_Transfer_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferDocument_Transfer_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Contact_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Transfer_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Agreement_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Unit_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "NewUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Unit_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "OldUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenu_Menu_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "MenuID",
                principalSchema: "MST",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDefaultProject_Project_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_User_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "ApproverID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_Role_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_User_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "ApproverID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_Role_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankBranch_BankBranchID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Company_CompanyID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_District_DistrictID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_Bank_BankID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_Company_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_BankBranch_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_Bank_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_Company_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_District_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_User_LCUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_Project_ProjectID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_Unit_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_Unit_UnitID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_User_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_Unit_UnitID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddressProject_Project_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_Project_ProjectID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_Project_ProjectID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_Project_ProjectID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Bank_BankID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_Contact_ContactID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_Project_ProjectID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposit_BankAccount_BankAccountID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_Company_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDetail_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_BankBranch_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_Bank_BankID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_Province_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_Bank_BankID",
                schema: "FIN",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_Project_ProjectID",
                schema: "FIN",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Booking_BookingID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBankTransfer_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_BankBranch_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_Bank_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_Company_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_Bank_BankID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_BankBranch_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_Bank_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_Company_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentQRCode_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Company_CompanyID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Contact_ContactID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_Company_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_Contact_ContactID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_Bank_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_BankAccount_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_DownPaymentLetter_Agreement_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferLetter_Agreement_AgreementID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileInstallation_User_UserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileNotification_User_UserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_WebNotification_User_UserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStory_Contact_ContactID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_Booking_BookingID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Project_ProjectID",
                schema: "PRM",
                table: "Promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionPreSale_Project_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_Quotation_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_Quotation_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Booking_BookingID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Contact_ContactID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Unit_UnitID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementDownPeriod_Agreement_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Agreement_AgreementID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Contact_ContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_AgencyID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_UnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingOwner_Booking_BookingID",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingOwner_Contact_ContactID",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_MortgageWithBank_Bank_BankID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Unit_UnitID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationCompare_Unit_UnitID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceive_TitledeedDetail_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceiveHistory_User_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Agreement_AgreementID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_LCID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Unit_UnitID",
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
                name: "FK_TransferCash_Transfer_TransferID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_BankBranch_BankBranchID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_Bank_BankID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_Transfer_TransferID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferDocument_Transfer_TransferID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Contact_ContactID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_Transfer_TransferID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Agreement_AgreementID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Unit_NewUnitID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_Unit_OldUnitID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenu_Menu_MenuID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDefaultProject_Project_ProjectID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_User_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_Role_RoleID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_User_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_Role_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "USR_RoleID");

            migrationBuilder.RenameColumn(
                name: "ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "USR_ApproverID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApproverTemplate_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "IX_WorkflowApproverTemplate_USR_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApproverTemplate_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                newName: "IX_WorkflowApproverTemplate_USR_ApproverID");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "USR_RoleID");

            migrationBuilder.RenameColumn(
                name: "ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "USR_ApproverID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApprover_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "IX_WorkflowApprover_USR_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowApprover_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                newName: "IX_WorkflowApprover_USR_ApproverID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_UserDefaultProject_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                newName: "IX_UserDefaultProject_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                newName: "MST_MenuID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteMenu_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                newName: "IX_FavoriteMenu_MST_MenuID");

            migrationBuilder.RenameColumn(
                name: "OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "PRJ_OldUnitID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "PRJ_NewUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_PRJ_OldUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferUnit_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                newName: "IX_TransferUnit_PRJ_NewUnitID");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "SAL_TransferID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferOwner_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "IX_TransferOwner_SAL_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferOwner_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                newName: "IX_TransferOwner_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "SAL",
                table: "TransferDocument",
                newName: "SAL_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferDocument_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                newName: "IX_TransferDocument_SAL_TransferID");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "SAL_TransferID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_SAL_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_BankID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCheque_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                newName: "IX_TransferCheque_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "SAL",
                table: "TransferCash",
                newName: "SAL_TransferID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_TransferID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_SAL_TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_BankID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferCash_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                newName: "IX_TransferCash_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "SAL",
                table: "Transfer",
                newName: "USR_LCID");

            migrationBuilder.RenameColumn(
                name: "LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "SAL",
                table: "Transfer",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_UnitID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_USR_LCID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_LCID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_AgreementID",
                schema: "SAL",
                table: "Transfer",
                newName: "IX_Transfer_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                newName: "USR_ActorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_TitledeedReceiveHistory_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                newName: "IX_TitledeedReceiveHistory_USR_ActorUserID");

            migrationBuilder.RenameColumn(
                name: "TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                newName: "PRJ_TitledeedDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_TitledeedReceive_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                newName: "IX_TitledeedReceive_PRJ_TitledeedDetailID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationCompare_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                newName: "IX_QuotationCompare_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "SAL",
                table: "Quotation",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Quotation_UnitID",
                schema: "SAL",
                table: "Quotation",
                newName: "IX_Quotation_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_MortgageWithBank_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                newName: "IX_MortgageWithBank_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "SAL_BookingID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingOwner_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "IX_BookingOwner_SAL_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingOwner_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                newName: "IX_BookingOwner_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "SAL",
                table: "Booking",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "SaleID",
                schema: "SAL",
                table: "Booking",
                newName: "USR_SaleID");

            migrationBuilder.RenameColumn(
                name: "SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                newName: "USR_SaleAtProjectID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "Booking",
                newName: "CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "AgencyID",
                schema: "SAL",
                table: "Booking",
                newName: "USR_AgencyID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UnitID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_SaleID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_USR_SaleID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_USR_SaleAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ContactID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_AgencyID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_USR_AgencyID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementOwner_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "IX_AgreementOwner_SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementOwner_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "IX_AgreementOwner_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_AgreementDownPeriod_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                newName: "IX_AgreementDownPeriod_SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "SAL",
                table: "Agreement",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_UnitID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "SAL_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_SAL_BookingID");

            migrationBuilder.RenameColumn(
                name: "QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                newName: "SAL_QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationTransferPromotion_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                newName: "IX_QuotationTransferPromotion_SAL_QuotationID");

            migrationBuilder.RenameColumn(
                name: "QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "SAL_QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationBookingPromotion_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "IX_QuotationBookingPromotion_SAL_QuotationID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionPreSale_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                newName: "IX_PromotionPreSale_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "PRM",
                table: "Promotion",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Promotion_ProjectID",
                schema: "PRM",
                table: "Promotion",
                newName: "IX_Promotion_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "SAL_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotion_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "IX_BookingPromotion_SAL_BookingID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "OST",
                table: "ContactStory",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactStory_ContactID",
                schema: "OST",
                table: "ContactStory",
                newName: "IX_ContactStory_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "NTF",
                table: "WebNotification",
                newName: "USR_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WebNotification_UserID",
                schema: "NTF",
                table: "WebNotification",
                newName: "IX_WebNotification_USR_UserID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "NTF",
                table: "MobileNotification",
                newName: "USR_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MobileNotification_UserID",
                schema: "NTF",
                table: "MobileNotification",
                newName: "IX_MobileNotification_USR_UserID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "NTF",
                table: "MobileInstallation",
                newName: "USR_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MobileInstallation_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                newName: "IX_MobileInstallation_USR_UserID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "LET",
                table: "TransferLetter",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferLetter_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                newName: "IX_TransferLetter_SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "SAL_AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_DownPaymentLetter_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "IX_DownPaymentLetter_SAL_AgreementID");

            migrationBuilder.RenameColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "MST_AttachFileFromBankID");

            migrationBuilder.RenameColumn(
                name: "AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "ACC_BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_MST_AttachFileFromBankID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_ACC_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTemp_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "IX_ReceiptTemp_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTemp_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                newName: "IX_ReceiptTemp_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "CTM_SendToContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendEmailHistory_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IX_ReceiptSendEmailHistory_CTM_SendToContactID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "FIN",
                table: "Receipt",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "FIN",
                table: "Receipt",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_ContactID",
                schema: "FIN",
                table: "Receipt",
                newName: "IX_Receipt_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_CompanyID",
                schema: "FIN",
                table: "Receipt",
                newName: "IX_Receipt_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                newName: "ACC_BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentQRCode_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                newName: "IX_PaymentQRCode_ACC_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "MST_PayToCompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_MST_PayToCompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPersonalCheque_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                newName: "IX_PaymentPersonalCheque_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentForeignBankTransfer_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IX_PaymentForeignBankTransfer_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCreditCard_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                newName: "IX_PaymentCreditCard_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "MST_PayToCompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_MST_PayToCompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCashierCheque_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                newName: "IX_PaymentCashierCheque_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                newName: "ACC_BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentBankTransfer_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                newName: "IX_PaymentBankTransfer_ACC_BankAccountID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "FIN",
                table: "Payment",
                newName: "SAL_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_BookingID",
                schema: "FIN",
                table: "Payment",
                newName: "IX_Payment_SAL_BookingID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "FIN",
                table: "EDC",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "EDC",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_EDC_ProjectID",
                schema: "FIN",
                table: "EDC",
                newName: "IX_EDC_PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_EDC_BankID",
                schema: "FIN",
                table: "EDC",
                newName: "IX_EDC_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "MST_ProvinceID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_MST_ProvinceID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectDebitDetail_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                newName: "IX_DirectDebitDetail_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDetail_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                newName: "IX_DirectCreditDetail_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExport_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "IX_DirectCreditDebitExport_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExport_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                newName: "IX_DirectCreditDebitExport_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "SAL_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_SAL_BookingID");

            migrationBuilder.RenameColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "Deposit",
                newName: "ACC_BankAccount");

            migrationBuilder.RenameIndex(
                name: "IX_Deposit_BankAccountID",
                schema: "FIN",
                table: "Deposit",
                newName: "IX_Deposit_ACC_BankAccount");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "CTM_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerWallet_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "IX_CustomerWallet_PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerWallet_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                newName: "IX_CustomerWallet_CTM_ContactID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_MST_BankID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "CTM",
                table: "Visitor",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitor_ProjectID",
                schema: "CTM",
                table: "Visitor",
                newName: "IX_Visitor_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "CTM",
                table: "Opportunity",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Opportunity_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                newName: "IX_Opportunity_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "CTM",
                table: "Lead",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_ProjectID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddressProject_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                newName: "IX_ContactAddressProject_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "USR_LCCTransferID");

            migrationBuilder.RenameColumn(
                name: "LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateTransfer_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "IX_CalculateTransfer_USR_LCCTransferID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateTransfer_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                newName: "IX_CalculateTransfer_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "USR_LCClosedDealID");

            migrationBuilder.RenameColumn(
                name: "LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "USR_LCAtProjectID");

            migrationBuilder.RenameColumn(
                name: "LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_USR_LCClosedDealID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_USR_LCAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateSale_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                newName: "IX_CalculateSale_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "USR_LCClosedDealUserID");

            migrationBuilder.RenameColumn(
                name: "LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "USR_LCAtProjectID");

            migrationBuilder.RenameColumn(
                name: "LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "PRJ_UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_USR_LCClosedDealUserID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_USR_LCAtProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatePerMonth_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                newName: "IX_CalculatePerMonth_PRJ_UnitID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "USR_LCUserID");

            migrationBuilder.RenameColumn(
                name: "LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "PRJ_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateOther_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "IX_CalculateOther_USR_LCUserID");

            migrationBuilder.RenameIndex(
                name: "IX_CalculateOther_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                newName: "IX_CalculateOther_PRJ_ProjectID");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "MST_DistrictID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_MST_DistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLDepositAccount_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                newName: "IX_PostGLDepositAccount_MST_BankBranchID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "MST_Bank");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLChartOfAccount_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "IX_PostGLChartOfAccount_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLChartOfAccount_BankID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                newName: "IX_PostGLChartOfAccount_MST_Bank");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                schema: "ACC",
                table: "BankAccount",
                newName: "MST_DistrictID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                schema: "ACC",
                table: "BankAccount",
                newName: "MST_CompanyID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "ACC",
                table: "BankAccount",
                newName: "MST_BankID");

            migrationBuilder.RenameColumn(
                name: "BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                newName: "MST_BankBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_MST_DistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_MST_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_BankID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_MST_BankID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_MST_BankBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankBranch_MST_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_MST_BankID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_District_MST_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_Bank_MST_Bank",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "MST_Bank",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_BankBranch_MST_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_Bank_MST_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_Company_MST_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_District_MST_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_Project_PRJ_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_User_USR_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "USR_LCUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "USR_LCAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_USR_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "USR_LCClosedDealUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                column: "USR_LCAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_USR_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                column: "USR_LCClosedDealID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_Unit_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_User_USR_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "USR_LCCTransferID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddressProject_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Lead",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_Project_PRJ_ProjectID",
                schema: "CTM",
                table: "Visitor",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Bank_MST_BankID",
                schema: "FIN",
                table: "BillPayment",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_Contact_CTM_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_Project_PRJ_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposit_BankAccount_ACC_BankAccount",
                schema: "FIN",
                table: "Deposit",
                column: "ACC_BankAccount",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_SAL_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "SAL_BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_Company_MST_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDetail_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_Bank_MST_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_Province_MST_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_Bank_MST_BankID",
                schema: "FIN",
                table: "EDC",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_Project_PRJ_ProjectID",
                schema: "FIN",
                table: "EDC",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Booking_SAL_BookingID",
                schema: "FIN",
                table: "Payment",
                column: "SAL_BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "ACC_BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_Company_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_PayToCompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_BankBranch_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_Bank_MST_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_Company_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_PayToCompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentQRCode_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "ACC_BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Contact_CTM_ContactID",
                schema: "FIN",
                table: "Receipt",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Company_MST_CompanyID",
                schema: "FIN",
                table: "Receipt",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_CTM_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "CTM_SendToContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_Contact_CTM_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_Company_MST_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "MST_CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_BankAccount_ACC_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "ACC_BankAccountID",
                principalSchema: "ACC",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_Bank_MST_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "MST_AttachFileFromBankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DownPaymentLetter_Agreement_SAL_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferLetter_Agreement_SAL_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileInstallation_User_USR_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "USR_UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileNotification_User_USR_UserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "USR_UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebNotification_User_USR_UserID",
                schema: "NTF",
                table: "WebNotification",
                column: "USR_UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStory_Contact_CTM_ContactID",
                schema: "OST",
                table: "ContactStory",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_Booking_SAL_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "SAL_BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Project_PRJ_ProjectID",
                schema: "PRM",
                table: "Promotion",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionPreSale_Project_PRJ_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_Quotation_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "SAL_QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_Quotation_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "SAL_QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_Booking_SAL_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "SAL_BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Contact_CTM_ContactID",
                schema: "SAL",
                table: "Agreement",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Agreement",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementDownPeriod_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_CTM_ContactID",
                schema: "SAL",
                table: "Booking",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Booking",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_USR_AgencyID",
                schema: "SAL",
                table: "Booking",
                column: "USR_AgencyID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                column: "USR_SaleAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_USR_SaleID",
                schema: "SAL",
                table: "Booking",
                column: "USR_SaleID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingOwner_Booking_SAL_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                column: "SAL_BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MortgageWithBank_Bank_MST_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Quotation",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationCompare_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceive_TitledeedDetail_PRJ_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "PRJ_TitledeedDetailID",
                principalSchema: "PRJ",
                principalTable: "TitledeedDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceiveHistory_User_USR_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "USR_ActorUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Transfer",
                column: "PRJ_UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "Transfer",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_USR_LCID",
                schema: "SAL",
                table: "Transfer",
                column: "USR_LCID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_BankBranch_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_Bank_MST_BankID",
                schema: "SAL",
                table: "TransferCash",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferCash",
                column: "SAL_TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_BankBranch_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                column: "MST_BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_Bank_MST_BankID",
                schema: "SAL",
                table: "TransferCheque",
                column: "MST_BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                column: "SAL_TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferDocument_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                column: "SAL_TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Contact_CTM_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CTM_ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_Transfer_SAL_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                column: "SAL_TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Unit_PRJ_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "PRJ_NewUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Unit_PRJ_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "PRJ_OldUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_Agreement_SAL_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                column: "SAL_AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenu_Menu_MST_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "MST_MenuID",
                principalSchema: "MST",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDefaultProject_Project_PRJ_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "PRJ_ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_User_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "USR_ApproverID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_Role_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "USR_RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_User_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "USR_ApproverID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_Role_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "USR_RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
