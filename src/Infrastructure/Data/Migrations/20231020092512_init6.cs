using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleSessions_Modules_ModuleName",
                table: "UserModuleSessions");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "UserModuleSessions");

            migrationBuilder.AlterColumn<string>(
                name: "ModuleName",
                table: "UserModuleSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleSessions_Modules_ModuleName",
                table: "UserModuleSessions",
                column: "ModuleName",
                principalTable: "Modules",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleSessions_Modules_ModuleName",
                table: "UserModuleSessions");

            migrationBuilder.AlterColumn<string>(
                name: "ModuleName",
                table: "UserModuleSessions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "ModuleId",
                table: "UserModuleSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleSessions_Modules_ModuleName",
                table: "UserModuleSessions",
                column: "ModuleName",
                principalTable: "Modules",
                principalColumn: "Name");
        }
    }
}
