using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class drop_col_IsUse_kim_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ACC",
                table: "CalendarLock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowStep",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "WorkflowApprover",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "WFL",
                table: "Workflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "UserRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "UserDefaultProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "TaskType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "RoleGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "Role",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "MyTask",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "FavoriteMenu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "AuthorizeRule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "UnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferDocument",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferCash",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TransferAgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "TitledeedReceive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "SignContractWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "QuotationCompare",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "CreditBanking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeUnitFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "CancelMemo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwnerAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "BookingOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "AgreementFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "SAL",
                table: "Agreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "TransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJobItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRRequestJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionVatRate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJobItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "PRCancelJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MappingAgreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "BookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Tower",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "RoundFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "PriceList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Model",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "MinPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Floor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRJ",
                table: "Address",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStoryType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStoryGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "UnitStory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStoryType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStoryGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "OST",
                table: "ContactStory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "WebNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "SmsNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "MobileNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "MobileInstallation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "EmailNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "TypeOfRealEstate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "SubDistrict",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "SubBG",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Province",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "MenuArea",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Menu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "LegalEntity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "LandOffice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "EDCFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "EDC",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "District",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Country",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "CancelReturnSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "CancelReason",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Brand",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "BG",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "BankBranch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "BankAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Bank",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "AgentEmployee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "Agent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "LET",
                table: "TransferLetter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptTempDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "ReceiptDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentMethodToItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentChangeUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "Payment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "FET",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "DepositHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "DepositDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "CustomerWallet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPaymentDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "FIN",
                table: "BillPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "DMT",
                table: "JobTransaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "Visitor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivityResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityAssign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivityResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "Opportunity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadScoringType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadScoring",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadAssign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "LeadActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactAddressProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ContactAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CTM",
                table: "ActivityTask",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingFixSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSettingAgent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "RateSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "IncreaseMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "DeductMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CommissionTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CommissionContract",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "ChangeLCTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "ChangeLCSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLHouseType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLDepositAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "GLExport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "GLDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ACC",
                table: "CalendarLock",
                nullable: false,
                defaultValue: false);
        }
    }
}
