using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KidsAppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioAnimals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnimalName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AudioFileUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioAnimals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AudioFileUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentUserName = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildUserAudioBooks",
                columns: table => new
                {
                    ChildUserId = table.Column<int>(type: "integer", nullable: false),
                    AudioBookId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildUserAudioBooks", x => new { x.ChildUserId, x.AudioBookId });
                    table.ForeignKey(
                        name: "FK_ChildUserAudioBooks_AudioBooks_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildUserAudioBooks_ChildUsers_ChildUserId",
                        column: x => x.ChildUserId,
                        principalTable: "ChildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: false),
                    GameType = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    DatePlayed = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResults_ChildUsers_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KidsModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ChildId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidsModes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KidsModes_ChildUsers_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: false),
                    StoryId = table.Column<int>(type: "integer", nullable: false),
                    CompletionPercentage = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryProgresses_ChildUsers_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AudioAnimals",
                columns: new[] { "Id", "AnimalName", "AudioFileUrl", "CreatedAt", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Cat", "http://example.com/audiofiles/cat_meow.mp3", new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2380), null, null },
                    { 2, "Dog", "http://example.com/audiofiles/dog_bark.mp3", new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2390), null, null }
                });

            migrationBuilder.InsertData(
                table: "AudioBooks",
                columns: new[] { "Id", "AudioFileUrl", "CreatedAt", "ModifiedDate", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "http://example.com/audiofiles/lionmouse.mp3", new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2370), null, "The Lion and The Mouse", null },
                    { 2, "http://example.com/audiofiles/redridinghood.mp3", new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2370), null, "Little Red Riding Hood", null }
                });

            migrationBuilder.InsertData(
                table: "ChildUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "ModifiedDate", "ParentUserName", "Password", "Score", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2200), "child1@example.com", null, "Parent1", "Test123", 0, null, "Child1" },
                    { 2, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2250), "child2@example.com", null, "Parent2", "Test123", 50, null, "Child2" }
                });

            migrationBuilder.InsertData(
                table: "ChildUserAudioBooks",
                columns: new[] { "AudioBookId", "ChildUserId", "CreatedAt", "Id", "IsDeleted", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2400), 0, false, null, null },
                    { 2, 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2400), 0, false, null, null },
                    { 1, 2, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2400), 0, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "GameResults",
                columns: new[] { "Id", "ChildId", "CreatedAt", "DatePlayed", "GameType", "ModifiedDate", "Score", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2330), new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2330), 0, null, 85, null },
                    { 2, 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2330), new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2330), 1, null, 90, null },
                    { 3, 2, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2340), new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2340), 0, null, 70, null }
                });

            migrationBuilder.InsertData(
                table: "KidsModes",
                columns: new[] { "Id", "ChildId", "CreatedAt", "Mode", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2310), "Girl", null, null },
                    { 2, 2, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2320), "Boy", null, null }
                });

            migrationBuilder.InsertData(
                table: "StoryProgresses",
                columns: new[] { "Id", "ChildId", "CompletionPercentage", "CreatedAt", "ModifiedDate", "StoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 50, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2350), null, 101, null },
                    { 2, 2, 100, new DateTime(2025, 1, 19, 9, 27, 14, 307, DateTimeKind.Local).AddTicks(2350), null, 102, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildUserAudioBooks_AudioBookId",
                table: "ChildUserAudioBooks",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_ChildId",
                table: "GameResults",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_KidsModes_ChildId",
                table: "KidsModes",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryProgresses_ChildId",
                table: "StoryProgresses",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioAnimals");

            migrationBuilder.DropTable(
                name: "ChildUserAudioBooks");

            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropTable(
                name: "KidsModes");

            migrationBuilder.DropTable(
                name: "StoryProgresses");

            migrationBuilder.DropTable(
                name: "AudioBooks");

            migrationBuilder.DropTable(
                name: "ChildUsers");
        }
    }
}
