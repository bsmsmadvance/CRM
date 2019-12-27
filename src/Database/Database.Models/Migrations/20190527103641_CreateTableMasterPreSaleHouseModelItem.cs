using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateTableMasterPreSaleHouseModelItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterPreSaleHouseModelItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterPreSalePromotionItemID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSaleHouseModelItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterPreSaleHouseModelItem_MasterPreSalePromotionItem_MasterPreSalePromotionItemID",
                        column: x => x.MasterPreSalePromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterPreSaleHouseModelItem_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSaleHouseModelItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "MasterPreSalePromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSaleHouseModelItem_ModelID",
                schema: "PRM",
                table: "MasterPreSaleHouseModelItem",
                column: "ModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterPreSaleHouseModelItem",
                schema: "PRM");
        }
    }
}
