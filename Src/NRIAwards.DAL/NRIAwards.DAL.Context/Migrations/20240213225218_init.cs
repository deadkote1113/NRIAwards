using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NRIAwards.DAL.Context.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisualContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisualContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    VisualContentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Awards_VisualContents_VisualContentId",
                        column: x => x.VisualContentId,
                        principalTable: "VisualContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    VisualContentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_VisualContents_VisualContentId",
                        column: x => x.VisualContentId,
                        principalTable: "VisualContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AwardEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AwardsId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    VisualContentId = table.Column<int>(type: "integer", nullable: false),
                    AwardId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardEvents_Awards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nominations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    AwardsId = table.Column<int>(type: "integer", nullable: false),
                    ReaderId = table.Column<int>(type: "integer", nullable: true),
                    VisualContentId = table.Column<int>(type: "integer", nullable: false),
                    AwardId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nominations_Awards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nominations_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nominations_VisualContents_VisualContentId",
                        column: x => x.VisualContentId,
                        principalTable: "VisualContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AwardSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ConnectionCode = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    PassedStagesCount = table.Column<int>(type: "integer", nullable: false),
                    AwardId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardSessions_Awards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nominees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    NominationId = table.Column<int>(type: "integer", nullable: false),
                    VisualContentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nominees_Nominations_NominationId",
                        column: x => x.NominationId,
                        principalTable: "Nominations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nominees_VisualContents_VisualContentId",
                        column: x => x.VisualContentId,
                        principalTable: "VisualContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomineeId = table.Column<int>(type: "integer", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    Tier = table.Column<int>(type: "integer", nullable: false),
                    TelegramUsername = table.Column<string>(type: "text", nullable: false),
                    TelegramAvatar = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Nominees_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "Nominees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VisualContents",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Link", "Title", "Type", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "", "Placeholder", 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsBlocked", "Login", "Password", "RoleId", "UpdatedAt", "VisualContentId" },
                values: new object[] { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "admin", "6fe7785e8523e09070fa676fa94c272b09c11699149a2a7589e67bf8ce81fd97ffb944005390f83e5eb1299383fc2b6c42bfc902e0daf106d64c3b574f68112f", 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AwardEvents_AwardId",
                table: "AwardEvents",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_VisualContentId",
                table: "Awards",
                column: "VisualContentId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardSessions_AwardId",
                table: "AwardSessions",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardSessions_UserId",
                table: "AwardSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_AwardId",
                table: "Nominations",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_ReaderId",
                table: "Nominations",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_VisualContentId",
                table: "Nominations",
                column: "VisualContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominees_NominationId",
                table: "Nominees",
                column: "NominationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominees_VisualContentId",
                table: "Nominees",
                column: "VisualContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VisualContentId",
                table: "Users",
                column: "VisualContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_NomineeId",
                table: "Votes",
                column: "NomineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardEvents");

            migrationBuilder.DropTable(
                name: "AwardSessions");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Nominees");

            migrationBuilder.DropTable(
                name: "Nominations");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "VisualContents");
        }
    }
}
