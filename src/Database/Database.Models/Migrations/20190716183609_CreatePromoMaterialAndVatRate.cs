using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromoMaterialAndVatRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionMaterial",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Plant = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    TypeCode = table.Column<string>(maxLength: 10, nullable: true),
                    TypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    PromotionMaterialGroupID = table.Column<Guid>(nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitPO = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    ValuationClass = table.Column<string>(maxLength: 100, nullable: true),
                    GLAccountNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMaterial", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionMaterial_PromotionMaterialGroup_PromotionMaterialGroupID",
                        column: x => x.PromotionMaterialGroupID,
                        principalSchema: "PRM",
                        principalTable: "PromotionMaterialGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionVatRate",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    VatRate = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionVatRate", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterial_PromotionMaterialGroupID",
                schema: "PRM",
                table: "PromotionMaterial",
                column: "PromotionMaterialGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionMaterial",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionVatRate",
                schema: "PRM");
        }
    }
}
