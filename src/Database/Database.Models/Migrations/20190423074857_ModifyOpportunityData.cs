using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyOpportunityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactSupervisor",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "LCOwner",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.RenameColumn(
                name: "VillageTH",
                schema: "CTM",
                table: "Lead",
                newName: "Village");

            migrationBuilder.RenameColumn(
                name: "SoiTH",
                schema: "CTM",
                table: "Lead",
                newName: "Soi");

            migrationBuilder.RenameColumn(
                name: "MooTH",
                schema: "CTM",
                table: "Lead",
                newName: "Moo");

            migrationBuilder.RenameColumn(
                name: "HouseNoTH",
                schema: "CTM",
                table: "Lead",
                newName: "HouseNo");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyReason",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                schema: "CTM",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductQTY",
                schema: "CTM",
                table: "Opportunity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompare",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CallBack",
                schema: "CTM",
                table: "Lead",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_OwnerID",
                schema: "CTM",
                table: "Visitor",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_OwnerID",
                schema: "CTM",
                table: "Opportunity",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_User_OwnerID",
                schema: "CTM",
                table: "Opportunity",
                column: "OwnerID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_User_OwnerID",
                schema: "CTM",
                table: "Visitor",
                column: "OwnerID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_User_OwnerID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_User_OwnerID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_OwnerID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_OwnerID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "BuyReason",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "ProductQTY",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "ProjectCompare",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "CTM",
                table: "Opportunity");

            migrationBuilder.RenameColumn(
                name: "Village",
                schema: "CTM",
                table: "Lead",
                newName: "VillageTH");

            migrationBuilder.RenameColumn(
                name: "Soi",
                schema: "CTM",
                table: "Lead",
                newName: "SoiTH");

            migrationBuilder.RenameColumn(
                name: "Moo",
                schema: "CTM",
                table: "Lead",
                newName: "MooTH");

            migrationBuilder.RenameColumn(
                name: "HouseNo",
                schema: "CTM",
                table: "Lead",
                newName: "HouseNoTH");

            migrationBuilder.AddColumn<string>(
                name: "ContactSupervisor",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LCOwner",
                schema: "CTM",
                table: "Opportunity",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CallBack",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
