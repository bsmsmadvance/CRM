using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateRefreshTokenAndSAPMaterialSyncJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SAPMaterialSyncJob",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobNo = table.Column<string>(maxLength: 50, nullable: true),
                    Progress = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Params = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPMaterialSyncJob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SAPMaterialSyncJob_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAPMaterialSyncJob_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "USR",
                columns: table => new
                {
                    Token = table.Column<string>(maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Expires = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SAPMaterialSyncJob_CreatedByUserID",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SAPMaterialSyncJob_UpdatedByUserID",
                schema: "PRM",
                table: "SAPMaterialSyncJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_CreatedByUserID",
                schema: "USR",
                table: "RefreshToken",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UpdatedByUserID",
                schema: "USR",
                table: "RefreshToken",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserID",
                schema: "USR",
                table: "RefreshToken",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SAPMaterialSyncJob",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "USR");
        }
    }
}
