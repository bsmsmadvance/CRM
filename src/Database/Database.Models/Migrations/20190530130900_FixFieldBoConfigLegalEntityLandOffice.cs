﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixFieldBoConfigLegalEntityLandOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "MST",
                table: "LegalEntity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictID",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TransferFeeRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_DistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_ProvinceID",
                schema: "MST",
                table: "LandOffice",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "SubDistrictID");

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_District_DistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_Province_ProvinceID",
                schema: "MST",
                table: "LandOffice",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_District_DistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_Province_ProvinceID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_DistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_ProvinceID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "DistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "TransferFeeRate",
                schema: "MST",
                table: "BOConfiguration");
        }
    }
}
