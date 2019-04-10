using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.Repositories
{
    public class RepoGame
    {
        public static void RepoCreateANewGame(string gamename, string[] firstnames, string[] lastnames, string locationname)
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

            for (int i = 0; i <= firstnames.Length; i++)
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
        }
            
    }
}
