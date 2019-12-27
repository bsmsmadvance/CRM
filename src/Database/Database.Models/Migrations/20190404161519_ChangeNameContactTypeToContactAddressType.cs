using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeNameContactTypeToContactAddressType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.RenameColumn(
                name: "ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ContactAddressTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ContactAddressTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactAddressTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ContactAddressTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactAddressTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.RenameColumn(
                name: "ContactAddressTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ContactTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ContactAddressTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ContactTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
