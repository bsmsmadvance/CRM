using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromotionMaterialAddPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionMaterialAddPrice",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PromotionMaterialGroupID = table.Column<Guid>(nullable: false),
                    LowRisePercent = table.Column<double>(nullable: false),
                    HighRisePercent = table.Column<double>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMaterialAddPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionMaterialAddPrice_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionMaterialAddPrice_PromotionMaterialGroup_PromotionMaterialGroupID",
                        column: x => x.PromotionMaterialGroupID,
                        principalSchema: "PRM",
                        principalTable: "PromotionMaterialGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionMaterialAddPrice_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialAddPrice_CreatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialAddPrice_PromotionMaterialGroupID",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                column: "PromotionMaterialGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialAddPrice_UpdatedByUserID",
                schema: "PRM",
                table: "PromotionMaterialAddPrice",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionMaterialAddPrice",
                schema: "PRM");
        }
    }
}
