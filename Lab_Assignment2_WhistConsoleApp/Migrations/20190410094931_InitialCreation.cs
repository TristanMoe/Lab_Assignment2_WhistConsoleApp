using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab_Assignment2_WhistConsoleApp.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GamesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Started = table.Column<bool>(nullable: false),
                    Ended = table.Column<bool>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GamesId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "GameRounds",
                columns: table => new
                {
                    GameRoundsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoundNumber = table.Column<int>(nullable: false),
                    DealerPosition = table.Column<int>(nullable: false),
                    Ended = table.Column<bool>(nullable: false),
                    Started = table.Column<bool>(nullable: false),
                    GamesId = table.Column<int>(nullable: false),
                    RoundTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRounds", x => x.GameRoundsId);
                    table.ForeignKey(
                        name: "FK_GameRounds_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "GamesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Games_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Games",
                        principalColumn: "GamesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    PlayerPosition = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GamesId = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => new { x.GamesId, x.PlayerPosition });
                    table.UniqueConstraint("AK_GamePlayers_PlayerPosition", x => x.PlayerPosition);
                    table.ForeignKey(
                        name: "FK_GamePlayers_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "GamesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundPlayers",
                columns: table => new
                {
                    PlayerPosition = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    GameRoundsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundPlayers", x => x.PlayerPosition);
                    table.ForeignKey(
                        name: "FK_GameRoundPlayers_GameRounds_GameRoundsId",
                        column: x => x.GameRoundsId,
                        principalTable: "GameRounds",
                        principalColumn: "GameRoundsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRoundPlayers_GamePlayers_PlayerPosition_Points",
                        columns: x => new { x.PlayerPosition, x.Points },
                        principalTable: "GamePlayers",
                        principalColumns: new[] { "GamesId", "PlayerPosition" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    GameRoundsId = table.Column<int>(nullable: false),
                    Tricks = table.Column<int>(nullable: false),
                    TricksWon = table.Column<int>(nullable: false),
                    Trump = table.Column<string>(nullable: true),
                    BidWinnerPositionId = table.Column<int>(nullable: false),
                    BidWinnerMatePositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.GameRoundsId);
                    table.ForeignKey(
                        name: "FK_Rounds_GameRounds_GameRoundsId",
                        column: x => x.GameRoundsId,
                        principalTable: "GameRounds",
                        principalColumn: "GameRoundsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rounds_GamePlayers_BidWinnerMatePositionId_GameRoundsId",
                        columns: x => new { x.BidWinnerMatePositionId, x.GameRoundsId },
                        principalTable: "GamePlayers",
                        principalColumns: new[] { "GamesId", "PlayerPosition" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rounds_GamePlayers_BidWinnerPositionId_GameRoundsId",
                        columns: x => new { x.BidWinnerPositionId, x.GameRoundsId },
                        principalTable: "GamePlayers",
                        principalColumns: new[] { "GamesId", "PlayerPosition" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_PlayerId",
                table: "GamePlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundPlayers_GameRoundsId",
                table: "GameRoundPlayers",
                column: "GameRoundsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundPlayers_PlayerPosition_Points",
                table: "GameRoundPlayers",
                columns: new[] { "PlayerPosition", "Points" });

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GamesId",
                table: "GameRounds",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_BidWinnerMatePositionId_GameRoundsId",
                table: "Rounds",
                columns: new[] { "BidWinnerMatePositionId", "GameRoundsId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_BidWinnerPositionId_GameRoundsId",
                table: "Rounds",
                columns: new[] { "BidWinnerPositionId", "GameRoundsId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoundPlayers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "GameRounds");

            migrationBuilder.DropTable(
                name: "GamePlayers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
