using System;
using System.Collections.Generic;
using System.Linq;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp
{
    public static class DataSeeder
    {
        public static void SeedData(this DataContext d)
        {
            if (d.Games.Any())
                return;
            var location = new Location() {Name = "Kaelderen"};
            var game = new Games()
            {
                Ended = false,
                Location = location,
                Name = "SuperWeebTanks",
                Started = true,
                Updated = DateTime.Now,
                GamePlayers = new List<GamePlayer>(),
                GameRounds = new List<GameRounds>()
            };

            var tristan = new Players() {FirstName = "Tristan", LastName = "Moller", GamePlayers = new List<GamePlayer>()};
            var martin = new Players() {FirstName = "Martin", LastName = "Jespersen", GamePlayers = new List<GamePlayer>() };
            var marcus = new Players() {FirstName = "Marcus", LastName = "Gasberg", GamePlayers = new List<GamePlayer>() };
            var mathias = new Players() {FirstName = "Mathias", LastName = "Hansen", GamePlayers = new List<GamePlayer>() };

            var theJedis = new Team() {Name = "TheJedis", Points = 2,GamePlayers = new List<GamePlayer>()};
            var memers = new Team() { Name = "Memers", Points = 3, GamePlayers = new List<GamePlayer>()};

            var tristanGp = new GamePlayer() {Game = game, Player = tristan, PlayerPosition = 1, Teams = theJedis, GRPs = new List<GameRoundPlayers>()};
            var martinGp = new GamePlayer() {Game = game, Player = martin, PlayerPosition = 2, Teams = theJedis, GRPs = new List<GameRoundPlayers>()};
            var marcusGp = new GamePlayer() {Game = game, Player = marcus, PlayerPosition = 3, Teams = memers, GRPs = new List<GameRoundPlayers>()};
            var mathiasGp = new GamePlayer() {Game = game, Player = mathias, PlayerPosition = 4, Teams = memers, GRPs = new List<GameRoundPlayers>()};

            var gameRound = new GameRounds() { DealerPosition = 1, Ended = false, Game = game, RoundNumber = 1, Started = true };
            var tristanGrp = new GameRoundPlayers() {GamePlayer = tristanGp, GameRound = gameRound, Points = 5};
            tristanGp.GRPs.Add(tristanGrp);
            var martinGrp = new GameRoundPlayers() {GamePlayer = martinGp, GameRound = gameRound, Points = 3};
            martinGp.GRPs.Add(martinGrp);
            var marcusGrp = new GameRoundPlayers() { GamePlayer = marcusGp, GameRound = gameRound, Points = 2 };
            marcusGp.GRPs.Add(marcusGrp);
            var mathiasGrp = new GameRoundPlayers() { GamePlayer = mathiasGp, GameRound = gameRound, Points = 4};
            mathiasGp.GRPs.Add(mathiasGrp);

            game.GamePlayers.AddRange(new List<GamePlayer>()
            {
                tristanGp,
                martinGp,
                marcusGp,
                mathiasGp
            });
            game.GameRounds.Add(gameRound);

            d.Games.Add(game);
            d.GameRounds.Add(gameRound);
            d.GameRoundPlayers.Add(tristanGrp);
            d.GameRoundPlayers.Add(martinGrp);
            d.GameRoundPlayers.Add(marcusGrp);
            d.GameRoundPlayers.Add(mathiasGrp);
            d.GamePlayers.Add(tristanGp);
            d.GamePlayers.Add(martinGp);
            d.GamePlayers.Add(marcusGp);
            d.GamePlayers.Add(mathiasGp);
            d.Players.Add(tristan);
            d.Players.Add(martin);
            d.Players.Add(marcus);
            d.Players.Add(mathias);
            d.Teams.Add(theJedis);
            d.Teams.Add(memers);
            d.Locations.Add(location);
            d.SaveChanges();
        }
    }
}
