using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateBGAndEditProjectFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BdID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Brand",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Company",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.RenameColumn(
                name: "SbuID",
                schema: "PRJ",
                table: "Project",
                newName: "SBUID");

            migrationBuilder.AlterColumn<Guid>(
                name: "SBUID",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MortgageBankID",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BGID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BrandID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectStatus",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubBGID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BG",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BGNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SBUID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BG", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BG_SBU_SBUID",
                        column: x => x.SBUID,
                        principalSchema: "MST",
                        principalTable: "SBU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubBG",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BGNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BGID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBG", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubBG_BG_BGID",
                        column: x => x.BGID,
                        principalSchema: "MST",
                        principalTable: "BG",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_BGID",
                schema: "PRJ",
                table: "Project",
                column: "BGID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_BrandID",
                schema: "PRJ",
                table: "Project",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CompanyID",
                schema: "PRJ",
                table: "Project",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MortgageBankID",
                schema: "PRJ",
                table: "Project",
                column: "MortgageBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProductTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProjectTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_SBUID",
                schema: "PRJ",
                table: "Project",
                column: "SBUID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_SubBGID",
                schema: "PRJ",
                table: "Project",
                column: "SubBGID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "ProductTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BG_SBUID",
                schema: "MST",
                table: "BG",
                column: "SBUID");

            migrationBuilder.CreateIndex(
                name: "IX_SubBG_BGID",
                schema: "MST",
                table: "SubBG",
                column: "BGID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItemTemplate_MasterCenter_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "ProductTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_BG_BGID",
                schema: "PRJ",
                table: "Project",
                column: "BGID",
                principalSchema: "MST",
                principalTable: "BG",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Brand_BrandID",
                schema: "PRJ",
                table: "Project",
                column: "BrandID",
                principalSchema: "MST",
                principalTable: "Brand",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Company_CompanyID",
                schema: "PRJ",
                table: "Project",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Bank_MortgageBankID",
                schema: "PRJ",
                table: "Project",
                column: "MortgageBankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProductTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_MasterCenter_ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project",
                column: "ProjectTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_SBU_SBUID",
                schema: "PRJ",
                table: "Project",
                column: "SBUID",
                principalSchema: "MST",
                principalTable: "SBU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_SubBG_SubBGID",
                schema: "PRJ",
                table: "Project",
                column: "SubBGID",
                principalSchema: "MST",
                principalTable: "SubBG",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItemTemplate_MasterCenter_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_BG_BGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Brand_BrandID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Company_CompanyID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Bank_MortgageBankID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_MasterCenter_ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_SBU_SBUID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_SubBG_SubBGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropTable(
                name: "SubBG",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "BG",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_Project_BGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_BrandID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_CompanyID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_MortgageBankID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_SBUID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_SubBGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItemTemplate_ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.DropColumn(
                name: "BGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "BrandID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectTypeMasterCenterID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SubBGID",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProductTypeMasterCenterID",
                schema: "PRJ",
                table: "PriceListItemTemplate");

            migrationBuilder.RenameColumn(
                name: "SBUID",
                schema: "PRJ",
                table: "Project",
                newName: "SbuID");

            migrationBuilder.AlterColumn<string>(
                name: "SbuID",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MortgageBankID",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BdID",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                nullable: false,
                defaultValue: 0);
        }
    }
}
