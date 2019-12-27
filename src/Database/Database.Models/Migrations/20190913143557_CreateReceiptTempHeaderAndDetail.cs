using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateReceiptTempHeaderAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptTemp",
                schema: "FIN");

            migrationBuilder.CreateTable(
                name: "ReceiptTempHeader",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ReceiptTempNo = table.Column<string>(maxLength: 100, nullable: true),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true),
                    CompanyNameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyNameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyHouseNoTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyHouseNoEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyBuildingTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyBuildingEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySoiTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySoiEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyRoadTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyRoadEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyProvinceEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyProvinceTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySubDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySubDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyPostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyTelephone = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyFax = table.Column<string>(maxLength: 50, nullable: true),
                    ContactTitle = table.Column<string>(maxLength: 100, nullable: true),
                    ContactFirstNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMiddleNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactTitleExtEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactFirstNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMiddleNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactHouseNoTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMooTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactVillageTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSoiTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactRoadTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactHouseNoEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMooEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactVillageEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSoiEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactRoadEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    ContactCountryTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactCountryEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactProvinceTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactProvinceEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSubDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSubDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    ProjectNo = table.Column<string>(maxLength: 1000, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 1000, nullable: true),
                    UnitNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTempHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptTempHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTempHeader_PaymentMethod_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTempHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTempDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ReceiptTempHeaderID = table.Column<Guid>(nullable: false),
                    PaymentMethodToItemID = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    DescriptionEN = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTempDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptTempDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTempDetail_PaymentMethodToItem_PaymentMethodToItemID",
                        column: x => x.PaymentMethodToItemID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethodToItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTempDetail_ReceiptTempHeader_ReceiptTempHeaderID",
                        column: x => x.ReceiptTempHeaderID,
                        principalSchema: "FIN",
                        principalTable: "ReceiptTempHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptTempDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempDetail_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempDetail_PaymentMethodToItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "PaymentMethodToItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempDetail_ReceiptTempHeaderID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "ReceiptTempHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempDetail_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_PaymentID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTempHeader_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptTempDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "ReceiptTempHeader",
                schema: "FIN");

            migrationBuilder.CreateTable(
                name: "ReceiptTemp",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CompanyID = table.Column<Guid>(nullable: true),
                    ContactID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    PaymentID = table.Column<Guid>(nullable: true),
                    ReceiptTempNo = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTemp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_PaymentID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "UpdatedByUserID");
        }
    }
}
