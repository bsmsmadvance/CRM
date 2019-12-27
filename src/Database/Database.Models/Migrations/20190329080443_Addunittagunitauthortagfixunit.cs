using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Addunittagunitauthortagfixunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Floor_FloorID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AlterColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "UnitArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<Guid>(
                name: "TowerID",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<double>(
                name: "SaleArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfPrivilege",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfParkingUnFix",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfParkingFix",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<bool>(
                name: "IsTransferWaterMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsTransferElectricMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorID",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "UnitTag",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitAuthorTag",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TagID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitAuthorTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitAuthorTag_UnitTag_TagID",
                        column: x => x.TagID,
                        principalSchema: "MST",
                        principalTable: "UnitTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitAuthorTag_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitAuthorTag_TagID",
                schema: "MST",
                table: "UnitAuthorTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitAuthorTag_UnitID",
                schema: "MST",
                table: "UnitAuthorTag",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Floor_FloorID",
                schema: "PRJ",
                table: "Unit",
                column: "FloorID",
                principalSchema: "PRJ",
                principalTable: "Floor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Floor_FloorID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropTable(
                name: "UnitAuthorTag",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "UnitTag",
                schema: "MST");

            migrationBuilder.AlterColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UnitArea",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TowerID",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SaleArea",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfPrivilege",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfParkingUnFix",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfParkingFix",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTransferWaterMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTransferElectricMeter",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorID",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Floor_FloorID",
                schema: "PRJ",
                table: "Unit",
                column: "FloorID",
                principalSchema: "PRJ",
                principalTable: "Floor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
