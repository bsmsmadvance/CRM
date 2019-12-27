using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateActivityTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTask",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    ContactFirstName = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactLastName = table.Column<string>(maxLength: 1000, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    ActivityTaskOverdueStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    OverdueDays = table.Column<int>(nullable: false),
                    RepeatCount = table.Column<int>(nullable: false),
                    ActivityTaskStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    ActivityTaskTopicMasterCenterID = table.Column<Guid>(nullable: true),
                    ActivityTaskTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    ActivityTypeName = table.Column<string>(maxLength: 100, nullable: true),
                    OwnerID = table.Column<Guid>(nullable: true),
                    LeadActivityID = table.Column<Guid>(nullable: true),
                    OpportunityActivityID = table.Column<Guid>(nullable: true),
                    RevisitActivityID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTask", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivityTask_MasterCenter_ActivityTaskOverdueStatusMasterCenterID",
                        column: x => x.ActivityTaskOverdueStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_MasterCenter_ActivityTaskStatusMasterCenterID",
                        column: x => x.ActivityTaskStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_MasterCenter_ActivityTaskTopicMasterCenterID",
                        column: x => x.ActivityTaskTopicMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_MasterCenter_ActivityTaskTypeMasterCenterID",
                        column: x => x.ActivityTaskTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_LeadActivity_LeadActivityID",
                        column: x => x.LeadActivityID,
                        principalSchema: "CTM",
                        principalTable: "LeadActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_OpportunityActivity_OpportunityActivityID",
                        column: x => x.OpportunityActivityID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_User_OwnerID",
                        column: x => x.OwnerID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTask_RevisitActivity_RevisitActivityID",
                        column: x => x.RevisitActivityID,
                        principalSchema: "CTM",
                        principalTable: "RevisitActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTaskUpdateOverdueJob",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Progress = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Params = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTaskUpdateOverdueJob", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_ActivityTaskOverdueStatusMasterCenterID",
                schema: "CTM",
                table: "ActivityTask",
                column: "ActivityTaskOverdueStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_ActivityTaskStatusMasterCenterID",
                schema: "CTM",
                table: "ActivityTask",
                column: "ActivityTaskStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_ActivityTaskTopicMasterCenterID",
                schema: "CTM",
                table: "ActivityTask",
                column: "ActivityTaskTopicMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_ActivityTaskTypeMasterCenterID",
                schema: "CTM",
                table: "ActivityTask",
                column: "ActivityTaskTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_LeadActivityID",
                schema: "CTM",
                table: "ActivityTask",
                column: "LeadActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_OpportunityActivityID",
                schema: "CTM",
                table: "ActivityTask",
                column: "OpportunityActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_OwnerID",
                schema: "CTM",
                table: "ActivityTask",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_ProjectID",
                schema: "CTM",
                table: "ActivityTask",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTask_RevisitActivityID",
                schema: "CTM",
                table: "ActivityTask",
                column: "RevisitActivityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTask",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "ActivityTaskUpdateOverdueJob",
                schema: "CTM");
        }
    }
}
