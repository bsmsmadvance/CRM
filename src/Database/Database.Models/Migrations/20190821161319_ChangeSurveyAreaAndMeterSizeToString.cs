using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeSurveyAreaAndMeterSizeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "WaterMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ElectricMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LandStatusNote",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ProjectSaleUserID",
                schema: "SAL",
                table: "Booking",
                column: "ProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SaleUserID",
                schema: "SAL",
                table: "Booking",
                column: "SaleUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_ProjectSaleUserID",
                schema: "SAL",
                table: "Booking",
                column: "ProjectSaleUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_SaleUserID",
                schema: "SAL",
                table: "Booking",
                column: "SaleUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_ProjectSaleUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_SaleUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ProjectSaleUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SaleUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "WaterMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ElectricMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LandStatusNote",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                column: "SaleAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SaleID",
                schema: "SAL",
                table: "Booking",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                column: "SaleAtProjectID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_SaleID",
                schema: "SAL",
                table: "Booking",
                column: "SaleID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
