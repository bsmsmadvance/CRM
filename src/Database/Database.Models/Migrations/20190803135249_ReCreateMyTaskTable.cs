using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReCreateMyTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task",
                schema: "USR");

            migrationBuilder.CreateTable(
                name: "MyTask",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: true),
                    FromUserID = table.Column<Guid>(nullable: true),
                    TaskTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTask", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MyTask_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTask_User_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTask_TaskType_TaskTypeID",
                        column: x => x.TaskTypeID,
                        principalSchema: "USR",
                        principalTable: "TaskType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyTask_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTask_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_CreatedByUserID",
                schema: "USR",
                table: "MyTask",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_FromUserID",
                schema: "USR",
                table: "MyTask",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_TaskTypeID",
                schema: "USR",
                table: "MyTask",
                column: "TaskTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_UpdatedByUserID",
                schema: "USR",
                table: "MyTask",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_UserID",
                schema: "USR",
                table: "MyTask",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTask",
                schema: "USR");

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    FromUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    TaskTypeID = table.Column<Guid>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    UserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Task_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_TaskType_TaskTypeID",
                        column: x => x.TaskTypeID,
                        principalSchema: "USR",
                        principalTable: "TaskType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatedByUserID",
                schema: "USR",
                table: "Task",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FromUserID",
                schema: "USR",
                table: "Task",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskTypeID",
                schema: "USR",
                table: "Task",
                column: "TaskTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_UpdatedByUserID",
                schema: "USR",
                table: "Task",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserID",
                schema: "USR",
                table: "Task",
                column: "UserID");
        }
    }
}
