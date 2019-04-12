using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lab_Assignment2_WhistConsoleApp.Repositories
{
    public class RepoGame
    {
        public DataContext _db;

        public RepoGame(DataContext db)
        {
            _db = db; 
        }

        public List<Games> GetAllGames()
        {
            var games = _db.Games
                .Include(g => g.Location)
                .Include(g => g.GamePlayers)
                .ThenInclude(gp => gp.Player)
                .Include(g => g.GameRounds)
                .ThenInclude(gr => gr.GRPs)
                .ToList();

            return games; 
        }

        public Games GetGame(string gamename)
        {
            var game = _db.Games
                .Include(g => g.Location)
                .Include(g => g.GamePlayers)
                .ThenInclude(gp => gp.Player)
                .Include(g => g.GameRounds)
                .ThenInclude(gr => gr.GRPs)
                .FirstOrDefault(g => g.Name == gamename); 
            if(game == null)
                throw new Exception("Game Was Not Found!");

            return game; 
        }

        public GameInformationEventArg RepoCreateANewGame(string gamename, string[] firstnames, string[] lastnames, string locationname)
        {
            //Create Game
            var game = new Games();
            game.Name = gamename;

            //Create Location
            var location = new Location();
            location.Name = locationname; 
            
            //Create Players 
            var players = new List<Players>();
            //Create GamePlayers
            var gameplayers = new List<GamePlayer>(); 

            for (int i = 0; i < firstnames.Length; i++)
            {
                //Players
                var player = new Players();
                player.FirstName = firstnames[i];
                player.LastName = lastnames[i];
                player.GamePlayers = new List<GamePlayer>();

                //GamePlayers
                var gameplayer = new GamePlayer();
                gameplayer.Player = player;
                gameplayer.Game = game;
                gameplayer.PlayerPosition = i + 1;
                gameplayer.GRPs = new List<GameRoundPlayers>();

                player.GamePlayers.Add(gameplayer);

                players.Add(player);
                gameplayers.Add(gameplayer);
            }

            //Start Game
            game.Location = location;
            game.Updated = DateTime.Now;
            game.Ended = false;
            game.Started = true;
            game.GamePlayers = gameplayers;
            game.GameRounds = new List<GameRounds>();

            _db.Players.AddRange(players);
            _db.GamePlayers.AddRange(gameplayers);
            _db.Locations.Add(location);
            _db.Games.Add(game); 

            _db.SaveChanges(); 
            
            //Create EventArg
            var eventArg = new GameInformationEventArg();
            eventArg.Game = game;

            return eventArg; 
        }
    }
}
