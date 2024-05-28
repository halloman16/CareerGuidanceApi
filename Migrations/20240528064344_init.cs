using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestoreCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestoreCodeValidBefore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveryCodeValidBefore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasPasswordResetRequest = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenValidBefore = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Modules_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserModuleSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModuleSessions_Modules_ModuleName",
                        column: x => x.ModuleName,
                        principalTable: "Modules",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserModuleSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SessionModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    MaxScore = table.Column<float>(type: "real", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: false),
                    RecordingFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModuleSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionModels_UserModuleSessions_UserModuleSessionId",
                        column: x => x.UserModuleSessionId,
                        principalTable: "UserModuleSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CreatorId",
                table: "Modules",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionModels_UserModuleSessionId",
                table: "SessionModels",
                column: "UserModuleSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleSessions_ModuleName",
                table: "UserModuleSessions",
                column: "ModuleName");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleSessions_UserId",
                table: "UserModuleSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true,
                filter: "[Token] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionModels");

            migrationBuilder.DropTable(
                name: "UserModuleSessions");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
