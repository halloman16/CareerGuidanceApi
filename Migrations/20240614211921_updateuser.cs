using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecoveryCodeValidBefore",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RestoreCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RestoreCodeValidBefore",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WasPasswordResetRequest",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecoveryCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecoveryCodeValidBefore",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestoreCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RestoreCodeValidBefore",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WasPasswordResetRequest",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
