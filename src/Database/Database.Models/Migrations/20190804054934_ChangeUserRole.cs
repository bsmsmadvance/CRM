using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_Project_ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "USR",
                table: "UserRole",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleID",
                schema: "USR",
                table: "UserRole",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_Project_ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "USR",
                table: "UserRole",
                column: "RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_Project_ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "USR",
                table: "UserRole",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleID",
                schema: "USR",
                table: "UserRole",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_Project_ProjectID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "USR",
                table: "UserRole",
                column: "RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
