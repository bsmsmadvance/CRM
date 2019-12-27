using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeLeadActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityTrack_OpportunityActivity_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack");

            migrationBuilder.DropColumn(
                name: "FileAttachmentID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "OtherReasons",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityTrack_OpportunityActivity_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                column: "OpportunityAcitivityID",
                principalSchema: "CTM",
                principalTable: "OpportunityActivity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityTrack_OpportunityActivity_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack");

            migrationBuilder.DropColumn(
                name: "Filename",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "OtherReasons",
                schema: "CTM",
                table: "OpportunityActivityTrack");

            migrationBuilder.DropColumn(
                name: "LeadStatusType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "FileAttachmentID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityTrack_OpportunityActivity_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                column: "OpportunityAcitivityID",
                principalSchema: "CTM",
                principalTable: "OpportunityActivity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
