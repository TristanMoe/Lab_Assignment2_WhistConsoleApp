using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp.Repositories
{
    public class RepoGame
    {
        public DbContext _db;

        public RepoGame(DbContext db)
        {
            _db = db; 
        }

        public GameInformationEventArg RepoCreateANewGame(string gamename, string[] firstnames, string[] lastnames, string locationname)
        {
            //Create Game
            var game = new Games();
            game.Name = gamename;

            //Create Location
            var location = new Location();
            location.Name = locationname; 
            //Attach location to game
            game.LocationId = location.LocationId; 
            
            //Create Players 
            var players = new List<Players>();
            //Create GamePlayers
            var gameplayers = new List<GamePlayers>(); 

            for (int i = 0; i < firstnames.Length; i++)
            {
                //Players
                var player = new Players();
                player.FirstName = firstnames[i];
                player.LastName = lastnames[i]; 
                players.Add(player);

                //GamePlayers
                var gameplayer = new GamePlayers();
                gameplayer.PlayerId = player.PlayerId;
                gameplayer.GamesId = game.GamesId;
                gameplayer.PlayerPosition = i; 
                gameplayers.Add(gameplayer);
            }

            //Start Game
            game.Updated = DateTime.Now;
            game.Ended = false;
            game.Started = true;

            //TODO: Missing DB adds 
            _db.SaveChanges(); 
            

            //Create EventArg
            var eventArg = new GameInformationEventArg();
            eventArg.Game = game;
            eventArg.GamePlayers = gameplayers;

            return eventArg; 
        }
            
    }
}
