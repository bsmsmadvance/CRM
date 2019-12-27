using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateSAPPromoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SAP_ZRFCMM01",
                schema: "PRM",
                columns: table => new
                {
                    WERKS = table.Column<string>(maxLength: 4, nullable: false),
                    MATNR = table.Column<string>(maxLength: 18, nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MTART = table.Column<string>(maxLength: 4, nullable: true),
                    MATKL = table.Column<string>(maxLength: 9, nullable: true),
                    MEINS = table.Column<string>(maxLength: 3, nullable: true),
                    BSTME = table.Column<string>(maxLength: 3, nullable: true),
                    MAKTX = table.Column<string>(maxLength: 40, nullable: true),
                    WGBEZ = table.Column<string>(maxLength: 20, nullable: true),
                    MTBEZ = table.Column<string>(maxLength: 25, nullable: true),
                    BKLAS = table.Column<string>(maxLength: 4, nullable: true),
                    KONTS = table.Column<string>(maxLength: 10, nullable: true),
                    MSEHT = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAP_ZRFCMM01", x => new { x.WERKS, x.MATNR });
                });

            migrationBuilder.CreateTable(
                name: "SAP_ZRFCMM02",
                schema: "PRM",
                columns: table => new
                {
                    WERKS = table.Column<string>(maxLength: 4, nullable: false),
                    EBELN = table.Column<string>(maxLength: 10, nullable: false),
                    EBELP = table.Column<string>(maxLength: 5, nullable: false),
                    MATNR = table.Column<string>(maxLength: 18, nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BUKRS = table.Column<string>(maxLength: 4, nullable: true),
                    LIFNR = table.Column<string>(maxLength: 10, nullable: true),
                    EKORG = table.Column<string>(maxLength: 4, nullable: true),
                    EKGRP = table.Column<string>(maxLength: 3, nullable: true),
                    ZTERM = table.Column<string>(maxLength: 4, nullable: true),
                    KDATB = table.Column<string>(maxLength: 8, nullable: true),
                    KDATE = table.Column<string>(maxLength: 8, nullable: true),
                    MAKTX = table.Column<string>(maxLength: 40, nullable: true),
                    VAKEY = table.Column<string>(maxLength: 100, nullable: true),
                    KNUMH = table.Column<string>(maxLength: 10, nullable: true),
                    ERDAT = table.Column<string>(maxLength: 8, nullable: true),
                    DATAB = table.Column<string>(maxLength: 8, nullable: true),
                    DATBI = table.Column<string>(maxLength: 8, nullable: true),
                    KBETR = table.Column<string>(maxLength: 11, nullable: true),
                    KMEIN = table.Column<string>(maxLength: 3, nullable: true),
                    MEINS = table.Column<string>(maxLength: 3, nullable: true),
                    LOEKZ = table.Column<string>(maxLength: 1, nullable: true),
                    SLTAX = table.Column<string>(maxLength: 2, nullable: true),
                    THUNT = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAP_ZRFCMM02", x => new { x.WERKS, x.MATNR, x.EBELN, x.EBELP });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SAP_ZRFCMM01",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "SAP_ZRFCMM02",
                schema: "PRM");
        }
    }
}
