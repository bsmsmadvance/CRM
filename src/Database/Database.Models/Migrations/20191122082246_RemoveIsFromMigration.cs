using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveIsFromMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "IsFromMigration",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.RenameColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "IsLock");

            migrationBuilder.RenameColumn(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IsLock");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowStep",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowStep",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowStep",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowStep",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowApprover",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowApprover",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowApprover",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowApprover",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "Workflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "Workflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "Workflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "Workflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserRole",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserDefaultProject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserDefaultProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserDefaultProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserDefaultProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserAuthorizeProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserAuthorizeProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserAuthorizeProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "User",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "User",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "User",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "TaskType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "TaskType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "TaskType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "TaskType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "RoleGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "RoleGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "RoleGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "RoleGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "Role",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "Role",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "Role",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "MyTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "MyTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "MyTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "MyTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "FavoriteMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "FavoriteMenu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "FavoriteMenu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "FavoriteMenu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "USR",
                table: "AuthorizeRule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "USR",
                table: "AuthorizeRule",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "USR",
                table: "AuthorizeRule",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "USR",
                table: "AuthorizeRule",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPriceInstallment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPriceInstallment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPriceInstallment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferDocument",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferDocument",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferDocument",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferDocument",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferCash",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferAgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferAgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferAgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferAgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Transfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Transfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Transfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TitledeedReceive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TitledeedReceive",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TitledeedReceive",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TitledeedReceive",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "SignContractWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "SignContractWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "SignContractWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "SignContractWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationUnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationUnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationUnitPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationCompare",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationCompare",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationCompare",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationCompare",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Quotation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Quotation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Quotation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "PriceListWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "PriceListWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "PriceListWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CreditBanking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CreditBanking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CreditBanking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CreditBanking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeUnitFile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeUnitFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeUnitFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeUnitFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CancelMemo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CancelMemo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CancelMemo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CancelMemo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Booking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Booking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Booking",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementFile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementFile",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Agreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Agreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Agreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJobItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionVatRate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionVatRate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionVatRate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionVatRate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJobItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MappingAgreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MappingAgreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MappingAgreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MappingAgreement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionRequest",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionExpense",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingCreditCardItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "WaiveQC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "WaiveQC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "WaiveQC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Tower",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Tower",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Tower",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Tower",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "RoundFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "RoundFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "RoundFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "RoomPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "RoomPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "RoomPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Project",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Project",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Project",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceList",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceList",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceList",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Model",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Model",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Model",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "MinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "MinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "MinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "HighRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "HighRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "HighRiseFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "FloorPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "FloorPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "FloorPlanImage",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Floor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Floor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Floor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Floor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "AgreementConfig",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "AgreementConfig",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "AgreementConfig",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Address",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Address",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Address",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStoryType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStoryType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStoryType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStoryGroup",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "WebNotification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "WebNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "WebNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "WebNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "SmsNotification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "SmsNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "SmsNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "SmsNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "MobileNotification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "MobileNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "MobileNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "MobileNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "MobileInstallation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "MobileInstallation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "MobileInstallation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "MobileInstallation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "EmailNotification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "EmailNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "EmailNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "EmailNotification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "TypeOfRealEstate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "TypeOfRealEstate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "TypeOfRealEstate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "TypeOfRealEstate",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "SubDistrict",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "SubDistrict",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "SubDistrict",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "SubDistrict",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "SubBG",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "SubBG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "SubBG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "SubBG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Province",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Province",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Province",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Province",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MenuArea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MenuArea",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MenuArea",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MenuArea",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Menu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Menu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Menu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MasterCenter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "LegalEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "LandOffice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "LandOffice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "LandOffice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "EDCFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "EDCFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "EDCFee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "EDC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "EDC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "EDC",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "District",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "District",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "District",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "District",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Country",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Country",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Country",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Company",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Company",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Company",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "CancelReturnSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "CancelReturnSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "CancelReturnSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "CancelReturnSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "CancelReason",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "CancelReason",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "CancelReason",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "CancelReason",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Brand",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Brand",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Brand",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BOConfiguration",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BOConfiguration",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BOConfiguration",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BG",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BG",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BankBranch",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BankBranch",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BankBranch",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Bank",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Bank",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Bank",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Bank",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "AgentEmployee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "AgentEmployee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "AgentEmployee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "AgentEmployee",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Agent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Agent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Agent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Agent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "LET",
                table: "TransferLetter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "LET",
                table: "TransferLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "LET",
                table: "TransferLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "LET",
                table: "TransferLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "LET",
                table: "DownPaymentLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "LET",
                table: "DownPaymentLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "LET",
                table: "DownPaymentLetter",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptTempDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptTempDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptTempDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptTempDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentQRCode",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentQRCode",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentQRCode",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentMethodToItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentMethodToItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentMethodToItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentMethodToItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentMethod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentMethod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentMethod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentDebitCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentDebitCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentDebitCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentChangeUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentChangeUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentChangeUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentChangeUnit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentBillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentBillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentBillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentBankTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "Payment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "Payment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "Payment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "FET",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "FET",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "FET",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DepositHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DepositHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DepositHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DepositHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DepositDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DepositDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DepositDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DepositDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "CustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "CustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "CustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "CustomerWallet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "DMT",
                table: "JobTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "DMT",
                table: "JobTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "DMT",
                table: "JobTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "DMT",
                table: "JobTransaction",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityAssign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivityResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadSyncJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadScoringType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadScoring",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadScoring",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadScoring",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadScoring",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadAssign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadAssign",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactPhone",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactEmail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactAddressProject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactAddressProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactAddressProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactAddressProject",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ActivityTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ActivityTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ActivityTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ActivityTask",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingAgent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingAgent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingAgent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingAgent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "IncreaseMoney",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "IncreaseMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "IncreaseMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "IncreaseMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "GeneralSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "GeneralSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "GeneralSetting",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "DeductMoney",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "DeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "DeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "DeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CommissionTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CommissionTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CommissionTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CommissionTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CommissionContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CommissionContract",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CommissionContract",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CommissionContract",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "ChangeLCTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "ChangeLCTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "ChangeLCTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "ChangeLCTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "ChangeLCSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "ChangeLCSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "ChangeLCSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "ChangeLCSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLHouseType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLHouseType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLHouseType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLHouseType",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLDepositAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLDepositAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLDepositAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLDepositAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "GLExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "GLExport",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "GLExport",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "GLExport",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "GLDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "GLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "GLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "GLDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "CalendarLock",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "CalendarLock",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "CalendarLock",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "CalendarLock",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendPrintingHistory_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "LockByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "LockByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "LockByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "LockByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendPrintingHistory_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendEmailHistory_LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferBankTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TransferAgreementOwner");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "SignContractWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CreditBankingPrintingHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeUnitWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeUnitFile");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "CancelMemo");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerPhone");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerEmail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerPhone");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerEmail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "AgreementFile");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "TransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "SAPMaterialSyncJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJobItemResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRRequestJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterialAddPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJobItemResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJobItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "PRCancelJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRM",
                table: "BookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "LockByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "LockDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "LockByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "LockDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DepositHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "DepositDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentTemp");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "RateSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "IncreaseMoney");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "DeductMoney");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CommissionTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "ChangeLCTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateLowRiseTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CMS",
                table: "CalculateHighRiseSale");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.RenameColumn(
                name: "IsLock",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "IsFromMigration");

            migrationBuilder.RenameColumn(
                name: "IsLock",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IsFromMigration");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowStep",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "WorkflowApprover",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "WFL",
                table: "Workflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserDefaultProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "TaskType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "RoleGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "Role",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "MyTask",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "FavoriteMenu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "USR",
                table: "AuthorizeRule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "UnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferDocument",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferCash",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TransferAgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "TitledeedReceive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "SignContractWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "QuotationCompare",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CreditBankingPrintingHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CreditBanking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeUnitFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "CancelMemo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwnerAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "BookingOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "AgreementFile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "SAL",
                table: "Agreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "TransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJobItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRRequestJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionVatRate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJobItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "PRCancelJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "MappingAgreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRM",
                table: "BookingCreditCardItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Tower",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "RoundFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "PriceList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Model",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "MinPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Floor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "PRJ",
                table: "Address",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStoryType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStoryGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "UnitStory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStoryType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStoryGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "OST",
                table: "ContactStory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "NTF",
                table: "WebNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "NTF",
                table: "SmsNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "NTF",
                table: "MobileNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "NTF",
                table: "MobileInstallation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "NTF",
                table: "EmailNotification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "TypeOfRealEstate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "SubDistrict",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "SubBG",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Province",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "MenuArea",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Menu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "LegalEntity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "LandOffice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "EDCFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "EDC",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "District",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Country",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "CancelReturnSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "CancelReason",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Brand",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "BG",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "BankBranch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "BankAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Bank",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "AgentEmployee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "MST",
                table: "Agent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "LET",
                table: "TransferLetter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptTempDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "ReceiptDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentMethodToItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentChangeUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "Payment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "FET",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DepositHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "DepositDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "CustomerWallet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPaymentDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "FIN",
                table: "BillPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "DMT",
                table: "JobTransaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Visitor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivityResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityAssign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivityResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Opportunity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadSyncJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadScoringType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadScoring",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadAssign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "LeadActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactPhone",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactEmail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactAddressProject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ContactAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CTM",
                table: "ActivityTask",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingFixSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSettingAgent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "RateSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "IncreaseMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "DeductMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CommissionTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CommissionContract",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "ChangeLCTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "ChangeLCSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLHouseType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLDepositAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "GLExport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "GLDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFromMigration",
                schema: "ACC",
                table: "CalendarLock",
                nullable: false,
                defaultValue: false);
        }
    }
}
