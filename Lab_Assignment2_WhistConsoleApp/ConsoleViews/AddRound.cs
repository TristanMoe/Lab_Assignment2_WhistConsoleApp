﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    public class AddRound
    {
        #region Properties 

        public event EventHandler<GameInformationEventArg> RoundAddedEvent;
        public event EventHandler<WinnerInformationEventArgs> WinnerFoundEvent;
        public InGameView InGameView { get; set; }
        public Games Game { get; set; }
        public string Trump { get; set; }
        private RepoGame _repoGame;

        #endregion

        #region Constructor

        public AddRound(InGameView inGameView, RepoGame repoGame)
        {
            InGameView = inGameView;
            _repoGame = repoGame;
            InGameView.AddRoundEvent += HandleAddRoundEvent;
            RoundAddedEvent += InGameView.HandleInGameEvents;
        }

        #endregion

        #region EventHandlers

        protected virtual void OnRoundAddedEvent(GameInformationEventArg e)
        {
            RoundAddedEvent?.Invoke(this, e);
        }

        protected virtual void OnWinnerFoundEvent(WinnerInformationEventArgs e)
        {
            WinnerFoundEvent?.Invoke(this, e);
        }

        private void HandleAddRoundEvent(object sender, GameInformationEventArg e)
        {
            Console.Clear();
            // Check received information

            var currentRound = new GameRounds {GRPs = new List<GameRoundPlayers>(), Started = true};
            try
            {
                if (e.Game == null)
                    throw new NullReferenceException("No gameinformation received!");

                Game = e.Game;
                _repoGame._db.Update(Game);
                currentRound.Game = Game;
                var lastGame = Game.GameRounds.LastOrDefault();
                currentRound.DealerPosition = lastGame?.DealerPosition + 1 ?? 1;
                currentRound.DealerPosition = (currentRound.DealerPosition < 5) ? currentRound.DealerPosition : 1;
                currentRound.RoundNumber = lastGame?.RoundNumber + 1 ?? 1;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            // Update points for each player
            Console.WriteLine($"Round is #{currentRound.RoundNumber}");
            Console.WriteLine($"Current dealer is {currentRound.DealerPosition}");
            Console.WriteLine("Enter game results:");

            foreach (var player in Game.GamePlayers)
            {
                Console.WriteLine($"{player.Player.FirstName} {player.Player.LastName} points:");
                try
                {
                    // Update points for the gameplayer
                    string result = Console.ReadLine();

                    int points = -1;

                    if (!string.IsNullOrEmpty(result))
                        points = int.Parse(result);


                    // update points for gameplayer's team
                    if (points > 6)
                    {
                        player.Teams.Points += (points - 6);

                        if (player.Teams.Points >= 5)
                        {
                            OnWinnerFoundEvent(new WinnerInformationEventArgs {WinnerTeam = player.Teams});
                            return;
                        }
                    }

                    //add a gameround player
                    var gameRoundPlayer = new GameRoundPlayers
                    {
                        GamePlayer = player, GameRound = currentRound, Points = points
                    };
                    player.GRPs.Add(gameRoundPlayer);
                    currentRound.GRPs.Add(gameRoundPlayer);
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                        throw new InputException("Trump card must be specified");

                    Trump = trump;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                }
            }

            // Add game round
            try
            {
                currentRound.Trump = Trump;
                currentRound.Ended = true;

                Game.GameRounds.Add(currentRound);
                _repoGame._db.GameRounds.Add(currentRound);
                _repoGame._db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            // Back to InGameView
            OnRoundAddedEvent(new GameInformationEventArg {Game = Game});
        }

        #endregion
    }
}
