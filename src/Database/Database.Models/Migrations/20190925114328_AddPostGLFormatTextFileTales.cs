using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPostGLFormatTextFileTales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLockHistory_User_UserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFile_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropTable(
                name: "PostGLFormatTextFile",
                schema: "ACC");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLockHistory_UserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.DropColumn(
                name: "UserID",
                schema: "ACC",
                table: "CalendarLockHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                schema: "ACC",
                table: "CalendarLock",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                schema: "ACC",
                table: "CalendarLock",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostGLFormatTextFileHeader",
                schema: "ACC",
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
                    FormatTextFileCode = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLFormatTextFileHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFileHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFileHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLock_CompanyID",
                schema: "ACC",
                table: "CalendarLock",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLock_UserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFileHeader_CreatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFileHeader_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFileHeader",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLock_Company_CompanyID",
                schema: "ACC",
                table: "CalendarLock",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLock_User_UserID",
                schema: "ACC",
                table: "CalendarLock",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFileHeader_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "FormatTextFileID",
                principalSchema: "ACC",
                principalTable: "PostGLFormatTextFileHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLock_Company_CompanyID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarLock_User_UserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFileHeader_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropTable(
                name: "PostGLFormatTextFileHeader",
                schema: "ACC");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLock_CompanyID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropIndex(
                name: "IX_CalendarLock_UserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.DropColumn(
                name: "UserID",
                schema: "ACC",
                table: "CalendarLock");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostGLFormatTextFile",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ColumnName = table.Column<string>(maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    FixValue = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    PostingKey = table.Column<string>(maxLength: 10, nullable: true),
                    PostingType = table.Column<string>(maxLength: 10, nullable: true),
                    Seq = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLFormatTextFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFile_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_UserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFile_CreatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFile_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFile",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarLockHistory_User_UserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFile_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "FormatTextFileID",
                principalSchema: "ACC",
                principalTable: "PostGLFormatTextFile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
