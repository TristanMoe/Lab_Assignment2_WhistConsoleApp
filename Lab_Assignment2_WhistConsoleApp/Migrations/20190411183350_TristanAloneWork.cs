using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab_Assignment2_WhistConsoleApp.Migrations
{
    public partial class TristanAloneWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameRoundPlayers",
                keyColumn: "GameRoundPlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameRoundPlayers",
                keyColumn: "GameRoundPlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameRoundPlayers",
                keyColumn: "GameRoundPlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameRoundPlayers",
                keyColumn: "GameRoundPlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GamePlayers",
                keyColumn: "GamePlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GamePlayers",
                keyColumn: "GamePlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GamePlayers",
                keyColumn: "GamePlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GamePlayers",
                keyColumn: "GamePlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameRounds",
                keyColumn: "GameRoundsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GamesId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Trump",
                table: "GameRounds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trump",
                table: "GameRounds");

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GamesId", "Ended", "LocationId", "Name", "Started", "Updated" },
                values: new object[] { 1, false, 1, "SuperWeebTanks", true, new DateTime(2019, 4, 11, 12, 0, 4, 755, DateTimeKind.Local).AddTicks(6931) });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Tristan", "Moller" },
                    { 2, "Martin", "Jespersen" },
                    { 3, "Marcus", "Gasberg" },
                    { 4, "Mathias", "Hansen" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Name", "Points" },
                values: new object[,]
                {
                    { 1, "TheJedis", 2 },
                    { 2, "Memers", 3 }
                });

            migrationBuilder.InsertData(
                table: "GamePlayers",
                columns: new[] { "GamePlayerId", "GamesId", "PlayerId", "PlayerPosition", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1 },
                    { 2, 1, 2, 2, 1 },
                    { 3, 1, 3, 3, 2 },
                    { 4, 1, 4, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "GameRounds",
                columns: new[] { "GameRoundsId", "DealerPosition", "Ended", "GamesId", "RoundNumber", "Started" },
                values: new object[] { 1, 1, false, 1, 1, true });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 1, "Kælderen" });

            migrationBuilder.InsertData(
                table: "GameRoundPlayers",
                columns: new[] { "GameRoundPlayerId", "GamePlayerId", "GameRoundId", "Points" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 2, 1, 3 },
                    { 3, 3, 1, 2 },
                    { 4, 4, 1, 3 }
                });
        }
    }
}
