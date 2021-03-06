﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
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
                .Include(t => t.Teams)
                .Include(g => g.GameRounds)
                .ThenInclude(gr => gr.GRPs)
                .FirstOrDefault(g => g.Name == gamename); 
           

            return game; 
        }

        public GameInformationEventArg RepoCreateANewGame(string gamename, string[] firstnames, string[] lastnames, string locationname,string[] teamnames)
        {
            //Create Game
            var game = new Games {Name = gamename};

            //Create Location
            var location = new Location {Name = locationname};

            //Attach location to gameb
            
            game.Location = location;

            //Create teams
            var teams = new List<Team>();
            for (int i = 0; i < teamnames.Length; i++)
            {
                var team = new Team {Name = teamnames[i]};
                teams.Add(team);
               
            }


            //Create Players 
            var players = new List<Players>();
            //Create GamePlayers
            var gameplayers = new List<GamePlayer>(); 

            for (int i = 0; i < firstnames.Length; i++)
            {
                //Players
                var player = new Players {FirstName = firstnames[i], LastName = lastnames[i]};


                players.Add(player);
            
                //GamePlayers
                var gameplayer = new GamePlayer
                {
                    Player = player,
                    Game = game,
                    PlayerPosition = i + 1,
                    GRPs = new List<GameRoundPlayers>(),
                    Teams = teams[(i / 2)]
                };




                players.Add(player);
                gameplayers.Add(gameplayer);
            }

            game.GameRounds=new List<GameRounds>();
            game.Teams = teams;

            game.GamePlayers = gameplayers;

            //Start Game
            game.Location = location;
            game.Updated = DateTime.Now;
            game.Ended = false;
            game.Started = true;
            
           /* foreach (var gameplayer in gameplayers)
            {
                _db.GamePlayers.Add(gameplayer);
            }
            _db.Teams.AddRange(teams);
            _db.Locations.Add(location);
            _db.Players.AddRange(players);*/
            _db.Games.Add(game);
            _db.SaveChanges();
           

            //Create EventArg
            var eventArg = new GameInformationEventArg {Game = game};

            return eventArg; 
        }
    }
}
