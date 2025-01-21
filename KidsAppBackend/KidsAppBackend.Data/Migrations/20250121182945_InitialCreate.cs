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
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ParentUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ChildUserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.ChildUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_ChildUsers_ChildUserId",
                        column: x => x.ChildUserId,
                        principalTable: "ChildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AudioAnimals",
                columns: new[] { "Id", "AnimalName", "AudioFileUrl", "CreatedAt", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Cat", "http://example.com/audiofiles/cat_meow.mp3", new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5840), null, null },
                    { 2, "Dog", "http://example.com/audiofiles/dog_bark.mp3", new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5840), null, null }
                });

            migrationBuilder.InsertData(
                table: "AudioBooks",
                columns: new[] { "Id", "AudioFileUrl", "CreatedAt", "ModifiedDate", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "http://example.com/audiofiles/lionmouse.mp3", new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5820), null, "The Lion and The Mouse", null },
                    { 2, "http://example.com/audiofiles/redridinghood.mp3", new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5820), null, "Little Red Riding Hood", null }
                });

            migrationBuilder.InsertData(
                table: "ChildUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "ModifiedDate", "ParentUserName", "Password", "Score", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5700), "child1@example.com", false, null, "Parent1", "Test123", 0, null, "Child1" },
                    { 2, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5740), "child2@example.com", false, null, "Parent2", "Test123", 50, null, "Child2" },
                    { 3, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5740), "parent1@example.com", false, null, "", "ParentPass123", 0, null, "Parent1" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "ModifiedDate", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Parent", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Child", null }
                });

            migrationBuilder.InsertData(
                table: "ChildUserAudioBooks",
                columns: new[] { "AudioBookId", "ChildUserId", "CreatedAt", "Id", "IsDeleted", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5850), 0, false, null, null },
                    { 2, 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5850), 0, false, null, null },
                    { 1, 2, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5850), 0, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "GameResults",
                columns: new[] { "Id", "ChildId", "CreatedAt", "DatePlayed", "GameType", "ModifiedDate", "Score", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5780), new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5780), 0, null, 85, null },
                    { 2, 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5780), new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5780), 1, null, 90, null },
                    { 3, 2, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5790), new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5790), 0, null, 70, null }
                });

            migrationBuilder.InsertData(
                table: "KidsModes",
                columns: new[] { "Id", "ChildId", "CreatedAt", "Mode", "ModifiedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5760), "Girl", null, null },
                    { 2, 2, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5760), "Boy", null, null }
                });

            migrationBuilder.InsertData(
                table: "StoryProgresses",
                columns: new[] { "Id", "ChildId", "CompletionPercentage", "CreatedAt", "ModifiedDate", "StoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 50, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5800), null, 101, null },
                    { 2, 2, 100, new DateTime(2025, 1, 21, 21, 29, 44, 815, DateTimeKind.Local).AddTicks(5800), null, 102, null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ChildUserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 }
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

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
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
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AudioBooks");

            migrationBuilder.DropTable(
                name: "ChildUsers");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
