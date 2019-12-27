using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromotionMaterialItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropTable(
                name: "PromotionMaterial",
                schema: "PRM");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                newName: "PromotionMaterialItemID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterTransferPromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                newName: "IX_MasterTransferPromotionItem_PromotionMaterialItemID");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                newName: "PromotionMaterialItemID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                newName: "IX_MasterPreSalePromotionItem_PromotionMaterialItemID");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                newName: "PromotionMaterialItemID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterBookingPromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                newName: "IX_MasterBookingPromotionItem_PromotionMaterialItemID");

            migrationBuilder.CreateTable(
                name: "PromotionMaterialItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    Plant = table.Column<string>(maxLength: 1000, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Unit = table.Column<string>(maxLength: 1000, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMaterialItem", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "PromotionMaterialItemID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterialItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionMaterialItemID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterialItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "PromotionMaterialItemID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterialItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_PromotionMaterialItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropTable(
                name: "PromotionMaterialItem",
                schema: "PRM");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                newName: "PromotionMaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterTransferPromotionItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                newName: "IX_MasterTransferPromotionItem_PromotionMaterialID");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                newName: "PromotionMaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                newName: "IX_MasterPreSalePromotionItem_PromotionMaterialID");

            migrationBuilder.RenameColumn(
                name: "PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                newName: "PromotionMaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_MasterBookingPromotionItem_PromotionMaterialItemID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                newName: "IX_MasterBookingPromotionItem_PromotionMaterialID");

            migrationBuilder.CreateTable(
                name: "PromotionMaterial",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    MaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    Plant = table.Column<string>(maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Unit = table.Column<string>(maxLength: 1000, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMaterial", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "PromotionMaterialID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterial",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionMaterialID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterial",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "PromotionMaterialID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterial",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
