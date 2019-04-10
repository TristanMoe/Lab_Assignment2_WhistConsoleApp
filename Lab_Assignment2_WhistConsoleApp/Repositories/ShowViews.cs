using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Remotion.Linq.Utilities;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class ShowViews
    {
        public DataContext _db { get; private set; }

        public ShowViews(DataContext db)
        {
            _db = db;
        }
        public async Task<Games> ListGameWithPlayers(int? id)
        {
            var games=await _db.Games
                .Include(players => players.GamePlayers)
                .ThenInclude(p=>p.Player)
                .SingleAsync(p=>p.GamesId==id);
            return games;
        }

        public async Task<Games> GetRoundInformation(int gamesId)
        {
            var game = await _db.Games
                .Include(d => d.GameRounds)
                    .ThenInclude(p => p.Game)
                        .ThenInclude(p => p.GamePlayers)
                            .ThenInclude(p => p.Player)
                .SingleAsync(p => p.GamesId == gamesId);
            return game;


            /* Console.WriteLine("Bids, result and points for every round:");
            
             foreach (var round in game.GameRounds)
             {              
                 Console.WriteLine($"Round :{round.RoundNumber}");
                 foreach (var player in round.GRPs)
                 {
                     Console.WriteLine($"{player.GamePlayer.Player.FirstName} {player.GamePlayer.Player.LastName}");
                     Console.WriteLine($"Bid: {player.}");
                     Console.WriteLine($"Result:");
                     Console.WriteLine($"Points:{player.Points}");
                 }
 
                 Console.WriteLine($"result of round {}");
             }
             //Tristan er langsom så funktionen er ikke færdig*/
        }

       /* public async Task<List<Games>> GameWinners(DataContext db)
        {
            var games=await db.Games
                .Include(p=>p.Wi)
        }*/

        public void CreateNewPlayer(Players player)
        {
            _db.Players.Add(player);
            //var game=new Games{Name=name,};
        }

        public async Task EditPlayer(int id)
        {
            var player = _db.Players.Single(p => p.PlayerId == id);
            _db.Update(player);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePlayer(int id)
        {
            var player = _db.Players.Single(p => p.PlayerId == id);
            _db.Remove(player);
            await _db.SaveChangesAsync();
        }

        public List<Players> GetNameOfPlayersInGameRound(int gameId)
        {
            List<Players> players = new List<Players>(); 

            var gamePlayers = _db.GamePlayers
                .Where(gp => gp.GamesId == gameId)
                .ToList();

            foreach (var gamePlayer in gamePlayers)
            {
                players = _db.Players
                    .Where(p => p.PlayerId == gamePlayer.PlayerId)
                    .ToList(); 
            }

            return players; 
        }

        public int CreateNewGame(string name, List<string> playersFirstnames, string locationName)
        {

            var game = new Games { Started = true, Ended = false, Updated = DateTime.Now, Name = name};
            var location = new Location {Name = locationName}; 
            var gameRounds = new GameRounds();
            game.Location = location;
            game.GameRounds = new List<GameRounds>();
            game.GamePlayers = new List<GamePlayers>();

            int i = 1; 
            //set foreign key for each gameplay (assumes that they exist in database)
            foreach (var playerName in playersFirstnames)
            {
                //Find Player
                var player = _db.Players
                    .Single(p => p.FirstName == playerName);


                var gamePlayer = new GamePlayers
                {
                    Player = player,
                    GamesId = game.GamesId,
                    PlayerPosition = i
                };

                _db.GamePlayers.Add(gamePlayer);
                game.GamePlayers.Add(gamePlayer);

                //Add new gameround player
                var gameRoundPlayer = new GameRoundPlayers();
                gameRoundPlayer.PlayerPosition = gamePlayer.PlayerPosition;

                //Add GameRoundPlayer to database
                _db.GameRoundPlayers.Add(gameRoundPlayer);

                i++; 
            }
            
            var entity = _db.Games.Add(game);
            _db.GameRounds.Add(gameRounds);
            _db.Locations.Add(location);
            
            _db.SaveChanges();

            return entity.Entity.GamesId;
        }

        public void AddPointsToGameRoundPlayer(int gameId, int points)
        {
            var gameRoundPlayer = _db.GameRoundPlayers
                .Include(grp => grp.GameRound).Where(grp => grp.GameRound.Game.GamesId == gameId)
                .FirstOrDefault(grp => grp.GameRound.GamesId == gameId);

            gameRoundPlayer.Points = points;

            _db.SaveChanges();
        }


        public void AddRound(int gameId, int tricks, int trickswon, string trump, string FN_winnerGameplayer, string FN_winnerMateGamePlayer)
        {
            var game = _db.Games
                .FirstOrDefault(g => g.GamesId == gameId);
            if (game == null)
                throw new Exception("ERROR, NOT FOUND");

            var gameround = _db.GameRounds
                .Include(gr => gr.Game)
                    .ThenInclude(g => g.GamePlayers)
                        .ThenInclude(p => p.Player)
                .FirstOrDefault(gr => gr.GamesId == game.GamesId);
            if (gameround == null)
                throw new Exception("ERROR, NOT FOUND");

            var gameplayers = _db.GamePlayers
                .Include(gp =>
                    gp.Player.FirstName == FN_winnerGameplayer && gp.Player.FirstName == FN_winnerMateGamePlayer)
                .ToList();

            foreach (var gamePlayer in gameplayers)
            {
                gamePlayer.Points += trickswon; 
            }

            var round = new Rounds
            {
                Tricks = tricks,
                TricksWon = trickswon,
                Trump = trump
            };

            _db.Rounds.Add(round);
            _db.SaveChanges();

        }

        public void EndGame(int gamesId)
        {
            //load game
            var game = _db.Games
                .FirstOrDefault(g => g.GamesId == gamesId);
            if (game == null)
                throw new Exception($"ERROR, NOT FOUND: {gamesId}");

            ////load game round players
            //var gameRoundPlayers = _db.GameRoundPlayers
            //    .Include(grp => grp.GameRound)
            //    .Where(grp => grp.GameRound.Game.GamesId == gamesId)
            //    .ToList();

            //foreach (var grp in gameRoundPlayers)
            //{
            //    var gamePlayer = _db.GamePlayers
            //        .FirstOrDefault(gp => gp.PlayerPosition == grp.PlayerPosition);
            //    if(gamePlayer == null)
            //        throw new Exception($"ERROR, NOT FOUND: Gameplayers");

            //    gamePlayer.Points += grp.Points; 
            //}
            game.Ended = true; 


        }
    }
}
