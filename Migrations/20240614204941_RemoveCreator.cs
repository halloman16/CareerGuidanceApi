using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_CreatorId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CreatorId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Modules");

            migrationBuilder.AddColumn<Guid>(
                name: "UserModelId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_UserModelId",
                table: "Modules",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_UserModelId",
                table: "Modules",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_UserModelId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_UserModelId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Modules");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CreatorId",
                table: "Modules",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_CreatorId",
                table: "Modules",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
