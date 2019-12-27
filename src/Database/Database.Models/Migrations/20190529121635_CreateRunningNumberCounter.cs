using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateRunningNumberCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionCodeCounter",
                schema: "PRM");

            migrationBuilder.CreateTable(
                name: "RunningNumberCounter",
                schema: "MST",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningNumberCounter", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RunningNumberCounter",
                schema: "MST");

            migrationBuilder.CreateTable(
                name: "PromotionCodeCounter",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterBookingPromotionNo = table.Column<int>(nullable: false),
                    MasterPreSalePromotionNo = table.Column<int>(nullable: false),
                    MasterTransferPromotionNo = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCodeCounter", x => x.ID);
                });
        }
    }
}
