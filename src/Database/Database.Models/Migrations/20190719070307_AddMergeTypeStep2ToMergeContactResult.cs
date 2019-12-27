using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMergeTypeStep2ToMergeContactResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SAP_ZRFCMM02",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.RenameColumn(
                name: "MatchMergeType",
                schema: "DMT",
                table: "MergeContactResult",
                newName: "MatchMergeTypeStep2");

            migrationBuilder.AlterColumn<string>(
                name: "KDATE",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KDATB",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DATBI",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DATAB",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MatchMergeTypeStep1",
                schema: "DMT",
                table: "MergeContactResult",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SAP_ZRFCMM02",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                columns: new[] { "WERKS", "MATNR", "EBELN", "EBELP", "KDATB", "KDATE", "DATAB", "DATBI" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SAP_ZRFCMM02",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "DocType",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Plant",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "DocType",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "Plant",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "BrandTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "DocType",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "GLAccountNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialBasePrice",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Plant",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "RemarkTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPVendor",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecEN",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SpecTH",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MatchMergeTypeStep1",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.RenameColumn(
                name: "MatchMergeTypeStep2",
                schema: "DMT",
                table: "MergeContactResult",
                newName: "MatchMergeType");

            migrationBuilder.AlterColumn<string>(
                name: "DATBI",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "DATAB",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "KDATE",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "KDATB",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SAP_ZRFCMM02",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                columns: new[] { "WERKS", "MATNR", "EBELN", "EBELP" });
        }
    }
}
