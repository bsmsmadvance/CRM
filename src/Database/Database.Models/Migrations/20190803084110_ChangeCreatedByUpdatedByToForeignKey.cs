using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeCreatedByUpdatedByToForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "Workflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "Workflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserRole",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserRole",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "TaskType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "TaskType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Tower",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Tower",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Floor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Floor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "SubBG",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "SubBG",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Province",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Province",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MenuArea",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MenuArea",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "District",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "District",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "CancelReason",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "CancelReason",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BG",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BG",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Bank",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Bank",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Agent",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Agent",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Receipt",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Receipt",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Deposit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Deposit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "GLExport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "GLExport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowType_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowType_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStepTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStepTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStep_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStep_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApproverTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApproverTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApprover_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApprover_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_CreatedByUserID",
                schema: "WFL",
                table: "Workflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_UpdatedByUserID",
                schema: "WFL",
                table: "Workflow",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CreatedByUserID",
                schema: "USR",
                table: "UserRole",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UpdatedByUserID",
                schema: "USR",
                table: "UserRole",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefaultProject_CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefaultProject_UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBackgroundJob_CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBackgroundJob_UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorizeProject_CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorizeProject_UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedByUserID",
                schema: "USR",
                table: "User",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedByUserID",
                schema: "USR",
                table: "User",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskType_CreatedByUserID",
                schema: "USR",
                table: "TaskType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskType_UpdatedByUserID",
                schema: "USR",
                table: "TaskType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatedByUserID",
                schema: "USR",
                table: "Task",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_UpdatedByUserID",
                schema: "USR",
                table: "Task",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroup_CreatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroup_UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedByUserID",
                schema: "USR",
                table: "Role",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedByUserID",
                schema: "USR",
                table: "Role",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenu_CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenu_UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRuleByRole_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRuleByRole_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRule_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRule_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrice_CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrice_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferUnit_CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferUnit_UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocument_CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocument_UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_CreatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_CreatedByUserID",
                schema: "SAL",
                table: "Transfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_UpdatedByUserID",
                schema: "SAL",
                table: "Transfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceiveHistory_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceiveHistory_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceive_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceive_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPrice_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPrice_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationCompare_CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationCompare_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_CreatedByUserID",
                schema: "SAL",
                table: "Quotation",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_UpdatedByUserID",
                schema: "SAL",
                table: "Quotation",
                column: "UpdatedByUserID");

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

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UpdatedByUserID",
                schema: "SAL",
                table: "Booking",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_CreatedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_UpdatedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionStockReceiveItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionStockReceiveItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDeliveryItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDeliveryItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDelivery_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDelivery_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAP_ZRFCMM02_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAP_ZRFCMM02_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAP_ZRFCMM01_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAP_ZRFCMM01_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionVatRate_CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionVatRate_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialItem_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialItem_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialGroup_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialGroup_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterial_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterial_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSaleHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSaleHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MappingAgreement_CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MappingAgreement_UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionStockReceiveItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionStockReceiveItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDeliveryItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDeliveryItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDelivery_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDelivery_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WaterElectricMeterPrice_CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WaterElectricMeterPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOtherUnitInfoTag_CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOtherUnitInfoTag_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CreatedByUserID",
                schema: "PRJ",
                table: "Unit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UpdatedByUserID",
                schema: "PRJ",
                table: "Unit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tower_CreatedByUserID",
                schema: "PRJ",
                table: "Tower",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tower_UpdatedByUserID",
                schema: "PRJ",
                table: "Tower",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAPWBSProSyncJob_CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAPWBSProSyncJob_UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlanImage_CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlanImage_UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CreatedByUserID",
                schema: "PRJ",
                table: "Project",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UpdatedByUserID",
                schema: "PRJ",
                table: "Project",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_CreatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OtherUnitInfoTag_CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OtherUnitInfoTag_UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_CreatedByUserID",
                schema: "PRJ",
                table: "Model",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_UpdatedByUserID",
                schema: "PRJ",
                table: "Model",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFenceFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFenceFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseBuildingPriceFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseBuildingPriceFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FloorPlanImage_CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FloorPlanImage_UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_CreatedByUserID",
                schema: "PRJ",
                table: "Floor",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_UpdatedByUserID",
                schema: "PRJ",
                table: "Floor",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncJob_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncJob_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItemResult_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItemResult_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotion_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotion_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPriceUnit_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPriceUnit_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPrice_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementConfig_CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementConfig_UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CreatedByUserID",
                schema: "PRJ",
                table: "Address",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UpdatedByUserID",
                schema: "PRJ",
                table: "Address",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStoryType_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStoryType_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStoryGroup_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStoryGroup_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStory_CreatedByUserID",
                schema: "OST",
                table: "UnitStory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStory_UpdatedByUserID",
                schema: "OST",
                table: "UnitStory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStoryType_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStoryType_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStoryGroup_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStoryGroup_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStory_CreatedByUserID",
                schema: "OST",
                table: "ContactStory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStory_UpdatedByUserID",
                schema: "OST",
                table: "ContactStory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WebNotification_CreatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WebNotification_UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SmsNotification_CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SmsNotification_UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplate_CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplate_UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNotification_CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNotification_UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileInstallation_CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileInstallation_UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotification_CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotification_UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfRealEstate_CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfRealEstate_UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDistrict_CreatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDistrict_UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubBG_CreatedByUserID",
                schema: "MST",
                table: "SubBG",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubBG_UpdatedByUserID",
                schema: "MST",
                table: "SubBG",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RunningNumberCounter_CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RunningNumberCounter_UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CreatedByUserID",
                schema: "MST",
                table: "Province",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Province_UpdatedByUserID",
                schema: "MST",
                table: "Province",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuArea_CreatedByUserID",
                schema: "MST",
                table: "MenuArea",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuArea_UpdatedByUserID",
                schema: "MST",
                table: "MenuArea",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_CreatedByUserID",
                schema: "MST",
                table: "Menu",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_UpdatedByUserID",
                schema: "MST",
                table: "Menu",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPriceItem_CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPriceItem_UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenterGroup_CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenterGroup_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenter_CreatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenter_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntity_CreatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntity_UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_CreatedByUserID",
                schema: "MST",
                table: "LandOffice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_UpdatedByUserID",
                schema: "MST",
                table: "LandOffice",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMessage_CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMessage_UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_CreatedByUserID",
                schema: "MST",
                table: "EDCFee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_UpdatedByUserID",
                schema: "MST",
                table: "EDCFee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_CreatedByUserID",
                schema: "MST",
                table: "EDC",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_UpdatedByUserID",
                schema: "MST",
                table: "EDC",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_District_CreatedByUserID",
                schema: "MST",
                table: "District",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_District_UpdatedByUserID",
                schema: "MST",
                table: "District",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatedByUserID",
                schema: "MST",
                table: "Country",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdatedByUserID",
                schema: "MST",
                table: "Country",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedByUserID",
                schema: "MST",
                table: "Company",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UpdatedByUserID",
                schema: "MST",
                table: "Company",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReturnSetting_CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReturnSetting_UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReason_CreatedByUserID",
                schema: "MST",
                table: "CancelReason",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReason_UpdatedByUserID",
                schema: "MST",
                table: "CancelReason",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CreatedByUserID",
                schema: "MST",
                table: "Brand",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UpdatedByUserID",
                schema: "MST",
                table: "Brand",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BOConfiguration_CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BOConfiguration_UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BG_CreatedByUserID",
                schema: "MST",
                table: "BG",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BG_UpdatedByUserID",
                schema: "MST",
                table: "BG",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_CreatedByUserID",
                schema: "MST",
                table: "BankBranch",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_UpdatedByUserID",
                schema: "MST",
                table: "BankBranch",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CreatedByUserID",
                schema: "MST",
                table: "BankAccount",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_UpdatedByUserID",
                schema: "MST",
                table: "BankAccount",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_CreatedByUserID",
                schema: "MST",
                table: "Bank",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_UpdatedByUserID",
                schema: "MST",
                table: "Bank",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgentEmployee_CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgentEmployee_UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_CreatedByUserID",
                schema: "MST",
                table: "Agent",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_UpdatedByUserID",
                schema: "MST",
                table: "Agent",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferLetter_CreatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferLetter_UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DownPaymentLetter_CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DownPaymentLetter_UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPayment_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPayment_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendPrintingHistory_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendPrintingHistory_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CreatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_UpdatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentQRCode_CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentQRCode_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodToItem_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodToItem_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCustomerWallet_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCustomerWallet_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBillPayment_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBillPayment_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreatedByUserID",
                schema: "FIN",
                table: "Payment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UpdatedByUserID",
                schema: "FIN",
                table: "Payment",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_CreatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_UpdatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWalletTransaction_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWalletTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWallet_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWallet_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_CreatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MergeContactResult_CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MergeContactResult_UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobTransaction_CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobTransaction_UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_CreatedByUserID",
                schema: "CTM",
                table: "Visitor",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_UpdatedByUserID",
                schema: "CTM",
                table: "Visitor",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityResult_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityResult_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivity_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivity_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVisitCounterSetting_CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVisitCounterSetting_UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityAssign_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityAssign_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityResult_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityResult_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_CreatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSyncJob_CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSyncJob_UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoringType_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoringType_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoring_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoring_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssign_CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssign_UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_CreatedByUserID",
                schema: "CTM",
                table: "Lead",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_UpdatedByUserID",
                schema: "CTM",
                table: "Lead",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhone_CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhone_UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddressProject_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddressProject_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CreatedByUserID",
                schema: "CTM",
                table: "Contact",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UpdatedByUserID",
                schema: "CTM",
                table: "Contact",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTaskUpdateOverdueJob_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTaskUpdateOverdueJob_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingTransfer_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSaleFix_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSaleFix_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSale_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSale_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateOnTop_CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateOnTop_UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSetting_CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSetting_UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHouseType_CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHouseType_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLChartOfAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLChartOfAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GLExport_CreatedByUserID",
                schema: "ACC",
                table: "GLExport",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GLExport_UpdatedByUserID",
                schema: "ACC",
                table: "GLExport",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GLDetail_CreatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GLDetail_UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                column: "UpdatedByUserID");

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
                name: "IX_CalendarLock_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLock_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLock_User_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLock_User_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLockHistory_User_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLockHistory_User_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GLDetail_User_CreatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GLDetail_User_UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GLExport_User_CreatedByUserID",
                schema: "ACC",
                table: "GLExport",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GLExport_User_UpdatedByUserID",
                schema: "ACC",
                table: "GLExport",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLChartOfAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDepositAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLHouseType_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLHouseType_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateOther_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatePerMonth_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateSale_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculateTransfer_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSetting_User_CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSetting_User_UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateOnTop_User_CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateOnTop_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingSale_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingSale_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingSaleFix_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingSaleFix_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingTransfer_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingTransfer_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTask_User_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTask_User_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTaskUpdateOverdueJob_User_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTaskUpdateOverdueJob_User_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_CreatedByUserID",
                schema: "CTM",
                table: "Contact",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UpdatedByUserID",
                schema: "CTM",
                table: "Contact",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddressProject_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddressProject_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactEmail_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactEmail_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhone_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhone_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_User_CreatedByUserID",
                schema: "CTM",
                table: "Lead",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_User_UpdatedByUserID",
                schema: "CTM",
                table: "Lead",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssign_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssign_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadScoring_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadScoring_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadScoringType_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadScoringType_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadSyncJob_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadSyncJob_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_User_CreatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_User_UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityResult_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityResult_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityAssign_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityAssign_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectVisitCounterSetting_User_CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectVisitCounterSetting_User_UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivityResult_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivityResult_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisitActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_User_CreatedByUserID",
                schema: "CTM",
                table: "Visitor",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_User_UpdatedByUserID",
                schema: "CTM",
                table: "Visitor",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTransaction_User_CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTransaction_User_UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MergeContactResult_User_CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MergeContactResult_User_UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_User_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWallet_User_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWalletTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWalletTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposit_User_CreatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposit_User_UpdatedByUserID",
                schema: "FIN",
                table: "Deposit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExport_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitUnitPriceItem_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitUnitPriceItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectDebitDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_CreatedByUserID",
                schema: "FIN",
                table: "Payment",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_UpdatedByUserID",
                schema: "FIN",
                table: "Payment",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBillPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBillPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCustomerWallet_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCustomerWallet_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDirectCreditDebit_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDirectCreditDebit_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItem_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodToItem_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodToItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentQRCode_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentQRCode_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_CreatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_UpdatedByUserID",
                schema: "FIN",
                table: "Receipt",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DownPaymentLetter_User_CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DownPaymentLetter_User_UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferLetter_User_CreatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferLetter_User_UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_User_CreatedByUserID",
                schema: "MST",
                table: "Agent",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_User_UpdatedByUserID",
                schema: "MST",
                table: "Agent",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentEmployee_User_CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentEmployee_User_UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bank_User_CreatedByUserID",
                schema: "MST",
                table: "Bank",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bank_User_UpdatedByUserID",
                schema: "MST",
                table: "Bank",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_User_CreatedByUserID",
                schema: "MST",
                table: "BankAccount",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_User_UpdatedByUserID",
                schema: "MST",
                table: "BankAccount",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankBranch_User_CreatedByUserID",
                schema: "MST",
                table: "BankBranch",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankBranch_User_UpdatedByUserID",
                schema: "MST",
                table: "BankBranch",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BG_User_CreatedByUserID",
                schema: "MST",
                table: "BG",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BG_User_UpdatedByUserID",
                schema: "MST",
                table: "BG",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BOConfiguration_User_CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BOConfiguration_User_UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_User_CreatedByUserID",
                schema: "MST",
                table: "Brand",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_User_UpdatedByUserID",
                schema: "MST",
                table: "Brand",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelReason_User_CreatedByUserID",
                schema: "MST",
                table: "CancelReason",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelReason_User_UpdatedByUserID",
                schema: "MST",
                table: "CancelReason",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelReturnSetting_User_CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelReturnSetting_User_UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_User_CreatedByUserID",
                schema: "MST",
                table: "Company",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_User_UpdatedByUserID",
                schema: "MST",
                table: "Company",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_User_CreatedByUserID",
                schema: "MST",
                table: "Country",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_User_UpdatedByUserID",
                schema: "MST",
                table: "Country",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_District_User_CreatedByUserID",
                schema: "MST",
                table: "District",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_District_User_UpdatedByUserID",
                schema: "MST",
                table: "District",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_User_CreatedByUserID",
                schema: "MST",
                table: "EDC",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_User_UpdatedByUserID",
                schema: "MST",
                table: "EDC",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_User_CreatedByUserID",
                schema: "MST",
                table: "EDCFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_User_UpdatedByUserID",
                schema: "MST",
                table: "EDCFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorMessage_User_CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorMessage_User_UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_User_CreatedByUserID",
                schema: "MST",
                table: "LandOffice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_User_UpdatedByUserID",
                schema: "MST",
                table: "LandOffice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalEntity_User_CreatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalEntity_User_UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenter_User_CreatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenter_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenterGroup_User_CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCenterGroup_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPriceItem_User_CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPriceItem_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_User_CreatedByUserID",
                schema: "MST",
                table: "Menu",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_User_UpdatedByUserID",
                schema: "MST",
                table: "Menu",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuArea_User_CreatedByUserID",
                schema: "MST",
                table: "MenuArea",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuArea_User_UpdatedByUserID",
                schema: "MST",
                table: "MenuArea",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Province_User_CreatedByUserID",
                schema: "MST",
                table: "Province",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Province_User_UpdatedByUserID",
                schema: "MST",
                table: "Province",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningNumberCounter_User_CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningNumberCounter_User_UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubBG_User_CreatedByUserID",
                schema: "MST",
                table: "SubBG",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubBG_User_UpdatedByUserID",
                schema: "MST",
                table: "SubBG",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubDistrict_User_CreatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubDistrict_User_UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfRealEstate_User_CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfRealEstate_User_UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileInstallation_User_CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileInstallation_User_UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplate_User_CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplate_User_UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmsNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmsNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStory_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStory_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStoryGroup_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStoryGroup_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStoryType_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactStoryType_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStory_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStory_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStoryGroup_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStoryGroup_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStoryType_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStoryType_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_CreatedByUserID",
                schema: "PRJ",
                table: "Address",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Address",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementConfig_User_CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementConfig_User_UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMinPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMinPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMinPriceUnit_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMinPriceUnit_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotion_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotion_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncItem_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncItem_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncItemResult_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncItemResult_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncJob_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionSyncJob_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_User_CreatedByUserID",
                schema: "PRJ",
                table: "Floor",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Floor",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorPlanImage_User_CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorPlanImage_User_UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseBuildingPriceFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseBuildingPriceFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_User_CreatedByUserID",
                schema: "PRJ",
                table: "Model",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Model",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherUnitInfoTag_User_CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherUnitInfoTag_User_UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItemTemplate_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItemTemplate_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_CreatedByUserID",
                schema: "PRJ",
                table: "Project",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Project",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPlanImage_User_CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPlanImage_User_UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAPWBSProSyncJob_User_CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAPWBSProSyncJob_User_UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_User_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_User_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_User_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_User_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tower_User_CreatedByUserID",
                schema: "PRJ",
                table: "Tower",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tower_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Tower",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_User_CreatedByUserID",
                schema: "PRJ",
                table: "Unit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Unit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOtherUnitInfoTag_User_CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOtherUnitInfoTag_User_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaiveQC_User_CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaiveQC_User_UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterElectricMeterPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterElectricMeterPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionDelivery_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionDelivery_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionDeliveryItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionDeliveryItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionStockReceiveItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionStockReceiveItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingAgreement_User_CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingAgreement_User_UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingCreditCardItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingCreditCardItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingHouseModelFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingHouseModelFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSaleHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSaleHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferCreditCardItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferCreditCardItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferHouseModelFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferHouseModelFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestUnit_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestUnit_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterial_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterial_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialGroup_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialGroup_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionVatRate_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionVatRate_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAP_ZRFCMM01_User_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAP_ZRFCMM01_User_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAP_ZRFCMM02_User_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SAP_ZRFCMM02_User_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionDelivery_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionDelivery_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionDeliveryItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionDeliveryItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionStockReceiveItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionStockReceiveItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_User_CreatedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_User_UpdatedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementDownPeriod_User_CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementDownPeriod_User_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_User_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_User_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_CreatedByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_UpdatedByUserID",
                schema: "SAL",
                table: "Booking",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_User_CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomer_User_UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MortgageWithBank_User_CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MortgageWithBank_User_UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_User_CreatedByUserID",
                schema: "SAL",
                table: "Quotation",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_User_UpdatedByUserID",
                schema: "SAL",
                table: "Quotation",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationCompare_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationCompare_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrice_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrice_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceive_User_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceive_User_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceiveHistory_User_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedReceiveHistory_User_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_CreatedByUserID",
                schema: "SAL",
                table: "Transfer",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_UpdatedByUserID",
                schema: "SAL",
                table: "Transfer",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCash_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferCheque_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferDocument_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferDocument_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferUnit_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPrice_User_CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPrice_User_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_User_CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPriceItem_User_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizeRule_User_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizeRule_User_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizeRuleByRole_User_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizeRuleByRole_User_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenu_User_CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenu_User_UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_CreatedByUserID",
                schema: "USR",
                table: "Role",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UpdatedByUserID",
                schema: "USR",
                table: "Role",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGroup_User_CreatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGroup_User_UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_CreatedByUserID",
                schema: "USR",
                table: "Task",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_UpdatedByUserID",
                schema: "USR",
                table: "Task",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskType_User_CreatedByUserID",
                schema: "USR",
                table: "TaskType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskType_User_UpdatedByUserID",
                schema: "USR",
                table: "TaskType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedByUserID",
                schema: "USR",
                table: "User",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdatedByUserID",
                schema: "USR",
                table: "User",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBackgroundJob_User_CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBackgroundJob_User_UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDefaultProject_User_CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDefaultProject_User_UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_CreatedByUserID",
                schema: "USR",
                table: "UserRole",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UpdatedByUserID",
                schema: "USR",
                table: "UserRole",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_CreatedByUserID",
                schema: "WFL",
                table: "Workflow",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_UpdatedByUserID",
                schema: "WFL",
                table: "Workflow",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApprover_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowApproverTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStep_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStep_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStepTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStepTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowType_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowType_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLock_User_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLock_User_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLockHistory_User_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLockHistory_User_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_GLDetail_User_CreatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_GLDetail_User_UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_GLExport_User_CreatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropForeignKey(
                name: "FK_GLExport_User_UpdatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLChartOfAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDepositAccount_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLHouseType_User_CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLHouseType_User_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateOther_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculatePerMonth_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateSale_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_User_CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculateTransfer_User_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSetting_User_CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSetting_User_UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_RateOnTop_User_CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropForeignKey(
                name: "FK_RateOnTop_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingSale_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingSale_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingSaleFix_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingSaleFix_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingTransfer_User_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingTransfer_User_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTask_User_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTask_User_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTaskUpdateOverdueJob_User_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTaskUpdateOverdueJob_User_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_CreatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UpdatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddressProject_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddressProject_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactEmail_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactEmail_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhone_User_CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhone_User_UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_User_CreatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_User_UpdatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssign_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssign_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadScoring_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadScoring_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadScoringType_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadScoringType_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadSyncJob_User_CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadSyncJob_User_UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_User_CreatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_User_UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityResult_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityResult_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityAssign_User_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityAssign_User_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectVisitCounterSetting_User_CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectVisitCounterSetting_User_UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivity_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivity_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivityResult_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivityResult_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivityStatus_User_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisitActivityStatus_User_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_User_CreatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_User_UpdatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTransaction_User_CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTransaction_User_UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_MergeContactResult_User_CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropForeignKey(
                name: "FK_MergeContactResult_User_UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_User_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWallet_User_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWalletTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWalletTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposit_User_CreatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposit_User_UpdatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExport_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitUnitPriceItem_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitUnitPriceItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectDebitDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_CreatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_UpdatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBankTransfer_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBankTransfer_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBillPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBillPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCustomerWallet_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCustomerWallet_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDirectCreditDebit_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDirectCreditDebit_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItem_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethodToItem_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethodToItem_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentQRCode_User_CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentQRCode_User_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_CreatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_UpdatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendPrintingHistory_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_User_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_User_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_DownPaymentLetter_User_CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_DownPaymentLetter_User_UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferLetter_User_CreatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferLetter_User_UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_User_CreatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_User_UpdatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentEmployee_User_CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentEmployee_User_UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Bank_User_CreatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropForeignKey(
                name: "FK_Bank_User_UpdatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_User_CreatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_User_UpdatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankBranch_User_CreatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_BankBranch_User_UpdatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_BG_User_CreatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropForeignKey(
                name: "FK_BG_User_UpdatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropForeignKey(
                name: "FK_BOConfiguration_User_CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_BOConfiguration_User_UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_User_CreatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_User_UpdatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_CancelReason_User_CreatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropForeignKey(
                name: "FK_CancelReason_User_UpdatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropForeignKey(
                name: "FK_CancelReturnSetting_User_CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_CancelReturnSetting_User_UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_User_CreatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_User_UpdatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_User_CreatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_User_UpdatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_District_User_CreatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_District_User_UpdatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_User_CreatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_User_UpdatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_User_CreatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_User_UpdatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorMessage_User_CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorMessage_User_UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_User_CreatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_User_UpdatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalEntity_User_CreatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalEntity_User_UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenter_User_CreatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenter_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenterGroup_User_CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCenterGroup_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPriceItem_User_CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPriceItem_User_UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_User_CreatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_User_UpdatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuArea_User_CreatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuArea_User_UpdatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_User_CreatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_User_UpdatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropForeignKey(
                name: "FK_RunningNumberCounter_User_CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropForeignKey(
                name: "FK_RunningNumberCounter_User_UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropForeignKey(
                name: "FK_SubBG_User_CreatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropForeignKey(
                name: "FK_SubBG_User_UpdatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDistrict_User_CreatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDistrict_User_UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfRealEstate_User_CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfRealEstate_User_UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileInstallation_User_CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileInstallation_User_UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplate_User_CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplate_User_UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_SmsNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_SmsNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_WebNotification_User_CreatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_WebNotification_User_UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStory_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStory_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStoryGroup_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStoryGroup_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStoryType_User_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactStoryType_User_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStory_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStory_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStoryGroup_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStoryGroup_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStoryType_User_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStoryType_User_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_CreatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementConfig_User_CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementConfig_User_UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMinPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMinPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMinPriceUnit_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMinPriceUnit_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotion_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotion_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncItem_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncItem_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncItemResult_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncItemResult_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncJob_User_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionSyncJob_User_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_User_CreatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorPlanImage_User_CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorPlanImage_User_UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseBuildingPriceFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseBuildingPriceFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_User_CreatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherUnitInfoTag_User_CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherUnitInfoTag_User_UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItemTemplate_User_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItemTemplate_User_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_CreatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPlanImage_User_CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPlanImage_User_UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_User_CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_User_UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_SAPWBSProSyncJob_User_CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_SAPWBSProSyncJob_User_UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_User_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_User_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_User_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_User_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Tower_User_CreatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropForeignKey(
                name: "FK_Tower_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_User_CreatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_User_UpdatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitOtherUnitInfoTag_User_CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitOtherUnitInfoTag_User_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WaiveQC_User_CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropForeignKey(
                name: "FK_WaiveQC_User_UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterElectricMeterPrice_User_CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterElectricMeterPrice_User_UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionDelivery_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionDelivery_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionDeliveryItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionDeliveryItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionStockReceiveItem_User_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionStockReceiveItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingAgreement_User_CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingAgreement_User_UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingCreditCardItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingCreditCardItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingHouseModelFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingHouseModelFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSaleHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSaleHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferCreditCardItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferCreditCardItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferHouseModelFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferHouseModelFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferHouseModelItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferHouseModelItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestUnit_User_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestUnit_User_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterial_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterial_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialGroup_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialGroup_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialItem_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionVatRate_User_CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionVatRate_User_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SAP_ZRFCMM01_User_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropForeignKey(
                name: "FK_SAP_ZRFCMM01_User_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropForeignKey(
                name: "FK_SAP_ZRFCMM02_User_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropForeignKey(
                name: "FK_SAP_ZRFCMM02_User_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionDelivery_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionDelivery_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionDeliveryItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionDeliveryItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionRequest_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionRequest_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionRequestItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionRequestItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionStockReceiveItem_User_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionStockReceiveItem_User_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_User_CreatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_User_UpdatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementDownPeriod_User_CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementDownPeriod_User_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_User_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_User_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_CreatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_UpdatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_User_CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomer_User_UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_MortgageWithBank_User_CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropForeignKey(
                name: "FK_MortgageWithBank_User_UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_User_CreatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_User_UpdatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationCompare_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationCompare_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrice_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrice_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_User_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_User_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceive_User_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceive_User_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceiveHistory_User_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedReceiveHistory_User_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_CreatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_UpdatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCash_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferCheque_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferDocument_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferDocument_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_User_CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferUnit_User_UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrice_User_CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrice_User_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_User_CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPriceItem_User_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizeRule_User_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizeRule_User_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizeRuleByRole_User_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizeRuleByRole_User_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenu_User_CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenu_User_UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_CreatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UpdatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleGroup_User_CreatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleGroup_User_UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_CreatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_UpdatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskType_User_CreatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskType_User_UpdatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBackgroundJob_User_CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBackgroundJob_User_UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDefaultProject_User_CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDefaultProject_User_UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_CreatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UpdatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_CreatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_UpdatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApprover_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowApproverTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStep_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStep_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStepTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStepTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTemplate_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTemplate_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowType_User_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowType_User_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowType_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowType_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStepTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStepTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStep_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStep_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowApproverTemplate_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowApproverTemplate_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowApprover_CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowApprover_UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_CreatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_UpdatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_CreatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UpdatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserDefaultProject_CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropIndex(
                name: "IX_UserDefaultProject_UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropIndex(
                name: "IX_UserBackgroundJob_CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropIndex(
                name: "IX_UserBackgroundJob_UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropIndex(
                name: "IX_UserAuthorizeProject_CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropIndex(
                name: "IX_UserAuthorizeProject_UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_TaskType_CreatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropIndex(
                name: "IX_TaskType_UpdatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropIndex(
                name: "IX_Task_CreatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_UpdatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_RoleGroup_CreatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropIndex(
                name: "IX_RoleGroup_UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropIndex(
                name: "IX_Role_CreatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UpdatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteMenu_CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteMenu_UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizeRuleByRole_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizeRuleByRole_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizeRule_CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizeRule_UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPriceItem_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_UnitPrice_CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_UnitPrice_UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_TransferUnit_CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropIndex(
                name: "IX_TransferUnit_UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferDocument_CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropIndex(
                name: "IX_TransferDocument_UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropIndex(
                name: "IX_TransferCheque_CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropIndex(
                name: "IX_TransferCheque_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropIndex(
                name: "IX_TransferCash_CreatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropIndex(
                name: "IX_TransferCash_UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_CreatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_UpdatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedReceiveHistory_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedReceiveHistory_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedReceive_CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedReceive_UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPrice_CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPrice_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_QuotationCompare_CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropIndex(
                name: "IX_QuotationCompare_UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_CreatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_UpdatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_MortgageWithBank_CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropIndex(
                name: "IX_MortgageWithBank_UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BookingCustomer_UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CreatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_UpdatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementDownPeriod_CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropIndex(
                name: "IX_AgreementDownPeriod_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_CreatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_UpdatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionStockReceiveItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionStockReceiveItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionDeliveryItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionDeliveryItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionDelivery_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionDelivery_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_SAP_ZRFCMM02_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropIndex(
                name: "IX_SAP_ZRFCMM02_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropIndex(
                name: "IX_SAP_ZRFCMM01_CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropIndex(
                name: "IX_SAP_ZRFCMM01_UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_QuotationPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_PromotionVatRate_CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropIndex(
                name: "IX_PromotionVatRate_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialItem_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialItem_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialGroup_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialGroup_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterial_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterial_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestUnit_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestUnit_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotion_CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotion_UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferHouseModelFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferHouseModelFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSaleHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSaleHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingHouseModelItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingHouseModelItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingHouseModelFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingHouseModelFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MappingAgreement_CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropIndex(
                name: "IX_MappingAgreement_UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionStockReceiveItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionStockReceiveItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionRequestItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionRequestItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionRequest_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionRequest_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionExpense_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionExpense_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionDeliveryItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionDeliveryItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionDelivery_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionDelivery_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotion_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotion_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_WaterElectricMeterPrice_CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropIndex(
                name: "IX_WaterElectricMeterPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropIndex(
                name: "IX_WaiveQC_CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropIndex(
                name: "IX_WaiveQC_UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropIndex(
                name: "IX_UnitOtherUnitInfoTag_CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropIndex(
                name: "IX_UnitOtherUnitInfoTag_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropIndex(
                name: "IX_Unit_CreatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_UpdatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Tower_CreatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropIndex(
                name: "IX_Tower_UpdatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_SAPWBSProSyncJob_CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_SAPWBSProSyncJob_UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoomPlanImage_CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropIndex(
                name: "IX_RoomPlanImage_UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropIndex(
                name: "IX_Project_CreatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UpdatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceList_CreatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropIndex(
                name: "IX_PriceList_UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropIndex(
                name: "IX_OtherUnitInfoTag_CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropIndex(
                name: "IX_OtherUnitInfoTag_UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropIndex(
                name: "IX_Model_CreatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_UpdatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_MinPrice_CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropIndex(
                name: "IX_MinPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFenceFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFenceFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseBuildingPriceFee_CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropIndex(
                name: "IX_LowRiseBuildingPriceFee_UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_FloorPlanImage_CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropIndex(
                name: "IX_FloorPlanImage_UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropIndex(
                name: "IX_Floor_CreatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Floor_UpdatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncJob_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncJob_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncItemResult_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncItemResult_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncItem_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionSyncItem_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotion_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotion_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BudgetMinPriceUnit_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropIndex(
                name: "IX_BudgetMinPriceUnit_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropIndex(
                name: "IX_BudgetMinPrice_CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropIndex(
                name: "IX_BudgetMinPrice_UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropIndex(
                name: "IX_AgreementConfig_CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropIndex(
                name: "IX_AgreementConfig_UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropIndex(
                name: "IX_Address_CreatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_UpdatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_UnitStoryType_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropIndex(
                name: "IX_UnitStoryType_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropIndex(
                name: "IX_UnitStoryGroup_CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropIndex(
                name: "IX_UnitStoryGroup_UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropIndex(
                name: "IX_UnitStory_CreatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropIndex(
                name: "IX_UnitStory_UpdatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropIndex(
                name: "IX_ContactStoryType_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropIndex(
                name: "IX_ContactStoryType_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropIndex(
                name: "IX_ContactStoryGroup_CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropIndex(
                name: "IX_ContactStoryGroup_UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropIndex(
                name: "IX_ContactStory_CreatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropIndex(
                name: "IX_ContactStory_UpdatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropIndex(
                name: "IX_WebNotification_CreatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropIndex(
                name: "IX_WebNotification_UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropIndex(
                name: "IX_SmsNotification_CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropIndex(
                name: "IX_SmsNotification_UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropIndex(
                name: "IX_NotificationTemplate_CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_NotificationTemplate_UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_MobileNotification_CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropIndex(
                name: "IX_MobileNotification_UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropIndex(
                name: "IX_MobileInstallation_CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropIndex(
                name: "IX_MobileInstallation_UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotification_CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotification_UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfRealEstate_CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfRealEstate_UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropIndex(
                name: "IX_SubDistrict_CreatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropIndex(
                name: "IX_SubDistrict_UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropIndex(
                name: "IX_SubBG_CreatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropIndex(
                name: "IX_SubBG_UpdatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropIndex(
                name: "IX_RunningNumberCounter_CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropIndex(
                name: "IX_RunningNumberCounter_UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropIndex(
                name: "IX_Province_CreatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropIndex(
                name: "IX_Province_UpdatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropIndex(
                name: "IX_MenuArea_CreatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropIndex(
                name: "IX_MenuArea_UpdatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropIndex(
                name: "IX_Menu_CreatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_UpdatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_MasterPriceItem_CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPriceItem_UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenterGroup_CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenterGroup_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenter_CreatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropIndex(
                name: "IX_MasterCenter_UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntity_CreatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntity_UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_CreatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_UpdatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_ErrorMessage_CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropIndex(
                name: "IX_ErrorMessage_UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_CreatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_UpdatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDC_CreatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropIndex(
                name: "IX_EDC_UpdatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropIndex(
                name: "IX_District_CreatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropIndex(
                name: "IX_District_UpdatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropIndex(
                name: "IX_Country_CreatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_UpdatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Company_CreatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_UpdatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_CancelReturnSetting_CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropIndex(
                name: "IX_CancelReturnSetting_UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropIndex(
                name: "IX_CancelReason_CreatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropIndex(
                name: "IX_CancelReason_UpdatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropIndex(
                name: "IX_Brand_CreatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_UpdatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_BOConfiguration_CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_BOConfiguration_UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_BG_CreatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropIndex(
                name: "IX_BG_UpdatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropIndex(
                name: "IX_BankBranch_CreatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_BankBranch_UpdatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_CreatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_UpdatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_Bank_CreatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_Bank_UpdatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_AgentEmployee_CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropIndex(
                name: "IX_AgentEmployee_UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropIndex(
                name: "IX_Agent_CreatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_UpdatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_TransferLetter_CreatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropIndex(
                name: "IX_TransferLetter_UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropIndex(
                name: "IX_DownPaymentLetter_CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropIndex(
                name: "IX_DownPaymentLetter_UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropIndex(
                name: "IX_UnknownPayment_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropIndex(
                name: "IX_UnknownPayment_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTemp_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTemp_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendPrintingHistory_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendPrintingHistory_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendEmailHistory_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendEmailHistory_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_CreatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_UpdatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_PaymentQRCode_CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropIndex(
                name: "IX_PaymentQRCode_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPersonalCheque_CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPersonalCheque_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethodToItem_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethodToItem_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_PaymentItem_CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentItem_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentForeignBankTransfer_CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_PaymentForeignBankTransfer_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDirectCreditDebit_CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDirectCreditDebit_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCustomerWallet_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCustomerWallet_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCreditCard_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCreditCard_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCashierCheque_CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCashierCheque_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBillPayment_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBillPayment_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBankTransfer_CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBankTransfer_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CreatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UpdatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_DirectDebitDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropIndex(
                name: "IX_DirectDebitDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitTransaction_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitExport_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitExport_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_Deposit_CreatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropIndex(
                name: "IX_Deposit_UpdatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropIndex(
                name: "IX_CustomerWalletTransaction_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropIndex(
                name: "IX_CustomerWalletTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropIndex(
                name: "IX_CustomerWallet_CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropIndex(
                name: "IX_CustomerWallet_UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropIndex(
                name: "IX_BillPaymentTransaction_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_BillPaymentTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_CreatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_MergeContactResult_CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropIndex(
                name: "IX_MergeContactResult_UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropIndex(
                name: "IX_JobTransaction_CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropIndex(
                name: "IX_JobTransaction_UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_CreatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_UpdatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivityResult_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivityResult_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivity_CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropIndex(
                name: "IX_RevisitActivity_UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropIndex(
                name: "IX_ProjectVisitCounterSetting_CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropIndex(
                name: "IX_ProjectVisitCounterSetting_UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityAssign_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityAssign_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivityResult_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivityResult_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivity_CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivity_UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_CreatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropIndex(
                name: "IX_LeadSyncJob_CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_LeadSyncJob_UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropIndex(
                name: "IX_LeadScoringType_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropIndex(
                name: "IX_LeadScoringType_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropIndex(
                name: "IX_LeadScoring_CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropIndex(
                name: "IX_LeadScoring_UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropIndex(
                name: "IX_LeadAssign_CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropIndex(
                name: "IX_LeadAssign_UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivityStatus_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivityStatus_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivity_CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivity_UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropIndex(
                name: "IX_Lead_CreatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_UpdatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_ContactPhone_CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropIndex(
                name: "IX_ContactPhone_UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropIndex(
                name: "IX_ContactEmail_CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropIndex(
                name: "IX_ContactEmail_UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddressProject_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddressProject_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CreatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_UpdatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTaskUpdateOverdueJob_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTaskUpdateOverdueJob_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTask_CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTask_UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingTransfer_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingSaleFix_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingSaleFix_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingSale_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingSale_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropIndex(
                name: "IX_RateOnTop_CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropIndex(
                name: "IX_RateOnTop_UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropIndex(
                name: "IX_GeneralSetting_CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropIndex(
                name: "IX_GeneralSetting_UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropIndex(
                name: "IX_CalculateTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropIndex(
                name: "IX_CalculateTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropIndex(
                name: "IX_CalculateSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropIndex(
                name: "IX_CalculateSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropIndex(
                name: "IX_CalculatePerMonth_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropIndex(
                name: "IX_CalculatePerMonth_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropIndex(
                name: "IX_CalculateOther_CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropIndex(
                name: "IX_CalculateOther_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropIndex(
                name: "IX_PostGLHouseType_CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropIndex(
                name: "IX_PostGLHouseType_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropIndex(
                name: "IX_PostGLDepositAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropIndex(
                name: "IX_PostGLDepositAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropIndex(
                name: "IX_PostGLChartOfAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropIndex(
                name: "IX_PostGLChartOfAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropIndex(
                name: "IX_PostGLAccount_CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropIndex(
                name: "IX_PostGLAccount_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropIndex(
                name: "IX_GLExport_CreatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropIndex(
                name: "IX_GLExport_UpdatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropIndex(
                name: "IX_GLDetail_CreatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropIndex(
                name: "IX_GLDetail_UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLockHistory_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLockHistory_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLock_CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLock_UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStepTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowStep");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApproverTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "WorkflowApprover");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "WFL",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserDefaultProject");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "TaskType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "RoleGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "FavoriteMenu");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRuleByRole");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "USR",
                table: "AuthorizeRule");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferUnit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferDocument");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferCheque");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TransferCash");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "TitledeedReceive");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "QuotationCompare");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "MortgageWithBank");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionVatRate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "MappingAgreement");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionRequest");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionDelivery");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "SAPWBSProSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "OtherUnitInfoTag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPriceUnit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStoryGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "UnitStory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStoryGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "OST",
                table: "ContactStory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "WebNotification");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "SmsNotification");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "MobileNotification");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "MobileInstallation");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "NTF",
                table: "EmailNotification");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "TypeOfRealEstate");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MenuArea");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "CancelReturnSetting");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "CancelReason");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "MST",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethodToItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCustomerWallet");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "Deposit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWalletTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "CustomerWallet");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "DMT",
                table: "JobTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivityResult");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "RevisitActivity");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ProjectVisitCounterSetting");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityAssign");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivityResult");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadSyncJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadScoring");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadAssign");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactEmail");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddressProject");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CTM",
                table: "ActivityTask");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHouseType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDepositAccount");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLChartOfAccount");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "GLExport");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "GLDetail");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowStep",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowStep",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "WorkflowApprover",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "WorkflowApprover",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WFL",
                table: "Workflow",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "WFL",
                table: "Workflow",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "UserRole",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserRole",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "UserDefaultProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserDefaultProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "UserAuthorizeProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "UserAuthorizeProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "User",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "User",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "TaskType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "TaskType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "Task",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "Task",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "RoleGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "RoleGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "Role",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "Role",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "FavoriteMenu",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "FavoriteMenu",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "USR",
                table: "AuthorizeRule",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "USR",
                table: "AuthorizeRule",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "UnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "UnitPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "UnitPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferDocument",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferDocument",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TransferCash",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "Transfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Transfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "TitledeedReceive",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "TitledeedReceive",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationUnitPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationUnitPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "QuotationCompare",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "QuotationCompare",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "Quotation",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Quotation",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "MortgageWithBank",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "MortgageWithBank",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "BookingCustomer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "Booking",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Booking",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "AgreementDownPeriod",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "AgreementDownPeriod",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "SAL",
                table: "Agreement",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "SAL",
                table: "Agreement",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionVatRate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionVatRate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterialGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "MappingAgreement",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "MappingAgreement",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionRequest",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionExpense",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "WaiveQC",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "WaiveQC",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "UnitOtherUnitInfoTag",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Unit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Unit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Tower",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Tower",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "SAPWBSProSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "RoundFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "RoundFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "RoomPlanImage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "RoomPlanImage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Project",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Project",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceListItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "PriceList",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "PriceList",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Model",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Model",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "MinPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "MinPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "HighRiseFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "HighRiseFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "FloorPlanImage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "FloorPlanImage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Floor",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Floor",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetMinPriceUnit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "AgreementConfig",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "AgreementConfig",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "PRJ",
                table: "Address",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "PRJ",
                table: "Address",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStoryType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStoryType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStoryGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStoryGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "UnitStory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "UnitStory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStoryType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStoryType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStoryGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStoryGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "OST",
                table: "ContactStory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "OST",
                table: "ContactStory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "WebNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "WebNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "SmsNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "SmsNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "NotificationTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "NotificationTemplate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "MobileNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "MobileNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "MobileInstallation",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "MobileInstallation",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "NTF",
                table: "EmailNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "NTF",
                table: "EmailNotification",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "TypeOfRealEstate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "TypeOfRealEstate",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "SubDistrict",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "SubDistrict",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "SubBG",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "SubBG",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "RunningNumberCounter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "RunningNumberCounter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Province",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Province",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "MenuArea",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "MenuArea",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Menu",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Menu",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterCenterGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterCenterGroup",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "LandOffice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "LandOffice",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "ErrorMessage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "ErrorMessage",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "EDCFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "EDCFee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "EDC",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "EDC",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "District",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "District",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Country",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Country",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Company",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Company",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "CancelReturnSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "CancelReturnSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "CancelReason",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "CancelReason",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Brand",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Brand",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "BOConfiguration",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "BOConfiguration",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "BG",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "BG",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "BankBranch",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "BankBranch",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "BankAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "BankAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Bank",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Bank",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "AgentEmployee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "AgentEmployee",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MST",
                table: "Agent",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MST",
                table: "Agent",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "LET",
                table: "TransferLetter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "LET",
                table: "TransferLetter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "LET",
                table: "DownPaymentLetter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "LET",
                table: "DownPaymentLetter",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptTemp",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptTemp",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "Receipt",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Receipt",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentQRCode",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentQRCode",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentMethodToItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentMethodToItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentMethod",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentMethod",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentBillPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentBillPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "PaymentBankTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "PaymentBankTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "Payment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Payment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectDebitDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectDebitDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "Deposit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "Deposit",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "CustomerWallet",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "CustomerWallet",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "DMT",
                table: "JobTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "DMT",
                table: "JobTransaction",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "Visitor",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Visitor",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivityResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivityResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "RevisitActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "RevisitActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ProjectVisitCounterSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityAssign",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityAssign",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivityResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivityResult",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadSyncJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadScoring",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadScoring",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadAssign",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadAssign",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadActivityStatus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "Lead",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Lead",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactPhone",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactPhone",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactEmail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactEmail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactAddressProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactAddressProject",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "Contact",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "Contact",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ActivityTaskUpdateOverdueJob",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CTM",
                table: "ActivityTask",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CTM",
                table: "ActivityTask",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingSaleFix",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingSaleFix",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateSettingSale",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateSettingSale",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "RateOnTop",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "RateOnTop",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "GeneralSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "GeneralSetting",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateTransfer",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateSale",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateSale",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculatePerMonth",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculatePerMonth",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "CMS",
                table: "CalculateOther",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "CMS",
                table: "CalculateOther",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLHouseType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLHouseType",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLDepositAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLDepositAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "PostGLAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "PostGLAccount",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "GLExport",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "GLExport",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "GLDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "GLDetail",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "CalendarLockHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "CalendarLockHistory",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ACC",
                table: "CalendarLock",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "ACC",
                table: "CalendarLock",
                maxLength: 450,
                nullable: true);
        }
    }
}
