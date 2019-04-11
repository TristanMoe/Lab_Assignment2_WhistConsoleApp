using System;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp
{
    public static class DataSeeder
    {
        public static void SeedData(this DataContext d)
        {

            d.Games.Add(
                new Games() { Ended = false, GamesId = 1, LocationId = 1, Name = "SuperWeebTanks", Started = true, Updated = DateTime.Now });
            d.GameRounds.Add(
                new GameRounds() { DealerPosition = 1, Ended = false, GameRoundsId = 1, GamesId = 1, RoundNumber = 1, Started = true });
            d.GameRoundPlayers.Add(
                new GameRoundPlayers() { GamePlayerId = 1, GameRoundPlayerId = 1, GameRoundId = 1, Points = 5 });
            d.GameRoundPlayers.Add(
                new GameRoundPlayers() { GamePlayerId = 2, GameRoundPlayerId = 2, GameRoundId = 1, Points = 3 });
            d.GameRoundPlayers.Add(
                new GameRoundPlayers() { GamePlayerId = 3, GameRoundPlayerId = 3, GameRoundId = 1, Points = 2 });
            d.GameRoundPlayers.Add(
                new GameRoundPlayers() { GamePlayerId = 4, GameRoundPlayerId = 4, GameRoundId = 1, Points = 3 });
            d.GamePlayers.Add(
                new GamePlayer() { GamePlayerId = 1, GamesId = 1, PlayerId = 1, PlayerPosition = 1, TeamId = 1 });
            d.GamePlayers.Add(
                new GamePlayer() { GamePlayerId = 2, GamesId = 1, PlayerId = 2, PlayerPosition = 2, TeamId = 1 });
            d.GamePlayers.Add(
                new GamePlayer() { GamePlayerId = 3, GamesId = 1, PlayerId = 3, PlayerPosition = 3, TeamId = 2 });
            d.GamePlayers.Add(
                new GamePlayer() { GamePlayerId = 4, GamesId = 1, PlayerId = 4, PlayerPosition = 4, TeamId = 2 });
            d.Players.Add(
                new Players() { FirstName = "Tristan", LastName = "Moller", PlayerId = 1 });
            d.Players.Add(
                new Players() { FirstName = "Martin", LastName = "Jespersen", PlayerId = 2 });
            d.Players.Add(
                new Players() { FirstName = "Marcus", LastName = "Gasberg", PlayerId = 3 });
            d.Players.Add(
                new Players() { FirstName = "Mathias", LastName = "Hansen", PlayerId = 4 });
            d.Teams.Add(
                new Team() { Name = "TheJedis", Points = 2, TeamId = 1 });
            d.Teams.Add(
                new Team() { Name = "Memers", Points = 3, TeamId = 2 });
            d.Locations.Add(
                new Location() { LocationId = 1, Name = "Kælderen" });
            d.SaveChanges();
        }
    }
}
