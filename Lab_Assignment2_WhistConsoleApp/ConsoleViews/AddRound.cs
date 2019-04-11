﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    class AddRound
    {
        #region Properties 

        public event EventHandler<GameInformationEventArg> RoundAddedEvent;
        public InGameView InGameView { get; set; }
        public Games Game { get; set; }
        public string Trump { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }
        private DataContext _db;

        #endregion

        #region Constructor

        public AddRound(InGameView inGameView, DataContext db)
        {
            InGameView = inGameView;
            _db = db;
            GamePlayers = new List<GamePlayer>();
            InGameView.AddRoundEvent += HandleAddRoundEvent;
        }

        #endregion

        #region EventHandlers

        protected virtual void OnRoundAddedEvent(GameInformationEventArg e)
        {
            RoundAddedEvent?.Invoke(this, e);
        }

        private void HandleAddRoundEvent(object sender, GameInformationEventArg e)
        {
            Console.Clear();
            // Check received information
            try
            {
                if (e.Game == null || e.GamePlayers == null)
                    throw new ArgumentNullException("No gameinformation received!");

                Game = e.Game;
                GamePlayers = e.GamePlayers;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            // Update points for each player
            Console.WriteLine("Enter game results:");

            foreach (var player in GamePlayers)
            {
                Console.WriteLine($"{player.Player.FirstName} {player.Player.LastName} points:");
                try
                {
                    // Update points for the gameroundplayer
                    string result = Console.ReadLine();

                    int points = -1;

                    if (!string.IsNullOrEmpty(result))
                        points = int.Parse(result);

                    if ((points > 13) || (points < 0))
                        throw new Exception("Number of points won must be between 0 and 13");

                    var gameRoundPlayer = _db.GameRoundPlayers.FirstOrDefault(grp => grp.GamePlayer.PlayerId == player.PlayerId);
                    if (gameRoundPlayer == null)
                        throw new Exception("No gameroundplayer found");

                    gameRoundPlayer.Points += points;

                    // Update points for the gameroundplayer's team
                    var team = _db.Teams.FirstOrDefault(t => t.GamePlayers.Contains(player));
                    if (team == null)
                        throw new Exception("No team found");

                    if (gameRoundPlayer.Points > 6)
                        team.Points += (gameRoundPlayer.Points - 6);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            // Enter trump card
            Console.WriteLine("Enter trump card");
            while (true)
            {
                try
                {
                    string trump = "";
                    trump = Console.ReadLine();
                    if (string.IsNullOrEmpty(trump))
                        throw new Exception("Trump card must be specified");

                    Trump = trump;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            // Add game round
            try
            {
                var game = _db.Games.FirstOrDefault(g => g.GamesId == Game.GamesId);
                if (game == null)
                    throw new Exception("Cannot add round, game not found");

                var gameRound = _db.GameRounds
                    .Include(gr => gr.GRPs)
                        .ThenInclude(grp => grp.GamePlayer)
                            .ThenInclude(gp => gp.Player)
                    .FirstOrDefault(gr => gr.GamesId == game.GamesId);

                if (gameRound == null)
                    throw new Exception("Cannot add round, gameround not found");

                gameRound.Trump = Trump;
                gameRound.Started = false;
                gameRound.Ended = true;

                // Adding new round
                int nextDealerPosition = (gameRound.DealerPosition + 1) <= 4 ? (gameRound.DealerPosition + 1) : 1;

                int nextRoundNumber = gameRound.RoundNumber + 1;
                if (nextRoundNumber > 13)
                {
                    // Go to winnerscreen
                }

                _db.GameRounds.Add(new GameRounds
                {
                    DealerPosition = nextDealerPosition, Ended = false, Started = true,
                    Game = game, GameRoundsId = gameRound.GameRoundsId+1, GamesId = game.GamesId,
                    GRPs = gameRound.GRPs, RoundNumber = nextRoundNumber, Trump = ""
                });
                
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // Back to InGameView
            Console.WriteLine("Added round successfully");
            OnRoundAddedEvent(new GameInformationEventArg {Game = Game, GamePlayers = GamePlayers});
        }

        #endregion
    }
}
