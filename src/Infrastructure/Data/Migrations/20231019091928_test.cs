using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModuleSessions_Modules_ModuleName",
                        column: x => x.ModuleName,
                        principalTable: "Modules",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_UserModuleSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Score = table.Column<float>(type: "REAL", nullable: false),
                    MaxScore = table.Column<float>(type: "REAL", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    Mark = table.Column<float>(type: "REAL", nullable: false),
                    DescriptionEvaluationReason = table.Column<string>(type: "TEXT", nullable: true),
                    RecordingFilename = table.Column<string>(type: "TEXT", nullable: true),
                    UserModuleSessionId = table.Column<Guid>(type: "TEXT", nullable: false)
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
        }
    }
}
