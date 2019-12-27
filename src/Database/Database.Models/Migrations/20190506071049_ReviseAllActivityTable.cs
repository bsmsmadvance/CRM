using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseAllActivityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "OpportunityActivityTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_OpportunityActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "IX_OpportunityActivity_OpportunityActivityTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                newName: "LeadActivityTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_LeadActivity_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                newName: "IX_LeadActivity_LeadActivityTypeMasterCenterID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_MasterCenter_LeadActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "LeadActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_OpportunityActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "OpportunityActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivity_MasterCenter_LeadActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_OpportunityActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.RenameColumn(
                name: "OpportunityActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "ActivityTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "ActivityDate");

            migrationBuilder.RenameIndex(
                name: "IX_OpportunityActivity_OpportunityActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                newName: "IX_OpportunityActivity_ActivityTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "LeadActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                newName: "ActivityTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_LeadActivity_LeadActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                newName: "IX_LeadActivity_ActivityTypeMasterCenterID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "CTM",
                table: "OpportunityActivity",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "CTM",
                table: "LeadActivity",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivity",
                column: "ActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_MasterCenter_ActivityTypeMasterCenterID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "ActivityTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
