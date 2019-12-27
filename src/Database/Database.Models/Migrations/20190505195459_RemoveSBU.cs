using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveSBU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BG_SBU_SBUID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_SBU_SBUID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_SBU_SBUID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropTable(
                name: "SBU",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_Project_SBUID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Brand_SBUID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_BG_SBUID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "SBUID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SBUID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "SBUID",
                schema: "MST",
                table: "BG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SBUID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SBUID",
                schema: "MST",
                table: "Brand",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SBUID",
                schema: "MST",
                table: "BG",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SBU",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    SBUNo = table.Column<string>(maxLength: 50, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SBU", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_SBUID",
                schema: "PRJ",
                table: "Project",
                column: "SBUID");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_SBUID",
                schema: "MST",
                table: "Brand",
                column: "SBUID");

            migrationBuilder.CreateIndex(
                name: "IX_BG_SBUID",
                schema: "MST",
                table: "BG",
                column: "SBUID");

            migrationBuilder.AddForeignKey(
                name: "FK_BG_SBU_SBUID",
                schema: "MST",
                table: "BG",
                column: "SBUID",
                principalSchema: "MST",
                principalTable: "SBU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_SBU_SBUID",
                schema: "MST",
                table: "Brand",
                column: "SBUID",
                principalSchema: "MST",
                principalTable: "SBU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_SBU_SBUID",
                schema: "PRJ",
                table: "Project",
                column: "SBUID",
                principalSchema: "MST",
                principalTable: "SBU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
