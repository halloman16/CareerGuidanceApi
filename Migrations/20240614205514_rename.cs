using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionModels_UserModuleSessions_UserModuleSessionId",
                table: "SessionModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionModels",
                table: "SessionModels");

            migrationBuilder.RenameTable(
                name: "SessionModels",
                newName: "Sessions");

            migrationBuilder.RenameIndex(
                name: "IX_SessionModels_UserModuleSessionId",
                table: "Sessions",
                newName: "IX_Sessions_UserModuleSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_UserModuleSessions_UserModuleSessionId",
                table: "Sessions",
                column: "UserModuleSessionId",
                principalTable: "UserModuleSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_UserModuleSessions_UserModuleSessionId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "SessionModels");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserModuleSessionId",
                table: "SessionModels",
                newName: "IX_SessionModels_UserModuleSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionModels",
                table: "SessionModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionModels_UserModuleSessions_UserModuleSessionId",
                table: "SessionModels",
                column: "UserModuleSessionId",
                principalTable: "UserModuleSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
