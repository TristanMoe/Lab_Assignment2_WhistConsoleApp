using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

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

            var currentRound = new GameRounds();
            currentRound.GRPs = new List<GameRoundPlayers>();
            currentRound.Started = true;
            try
            {
                if (e.Game == null)
                    throw new NullReferenceException("No gameinformation received!");

                Game = e.Game;
                _repoGame._db.Update(Game);
                Game.GameRounds.Add(currentRound);
                currentRound.Game = Game;
                var lastGame = Game.GameRounds.Last();
                if(lastGame == null)
                    throw new NullReferenceException();
                currentRound.DealerPosition = lastGame.DealerPosition + 1;
                currentRound.RoundNumber = lastGame.RoundNumber + 1;
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
                    // Update points for the gameroundplayer
                    string result = Console.ReadLine();

                    int points = -1;

                    if (!string.IsNullOrEmpty(result))
                        points = int.Parse(result);

                    if ((points > 13) || (points < 0))
                        throw new InputException("Number of points won must be between 0 and 13");

                    //add a gameround player
                    var gameRoundPlayer = new GameRoundPlayers();
                    gameRoundPlayer.GamePlayer = player;
                    gameRoundPlayer.GameRound = currentRound;
                    gameRoundPlayer.Points = points;
                    player.GRPs.Add(gameRoundPlayer);
                    currentRound.GRPs.Add(gameRoundPlayer);

                    // Update points for the gameroundplayer's team
                    //var team = gameRoundPlayer.GamePlayer.Teams;
                    //if (team == null)
                    //    throw new Exception("No team found");

                    //if (gameRoundPlayer.Points > 6)
                    //{
                    //    team.Points += (gameRoundPlayer.Points - 6);

                    //    // raise winnerfound event, to go winnerview
                    //    if (team.Points >= 5)
                    //    {
                    //        OnWinnerFoundEvent(new WinnerInformationEventArgs {WinnerTeam = team});
                    //        return;
                    //    }
                    //}
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
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

                _repoGame._db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            // Back to InGameView
            Console.WriteLine("Added round successfully");
            OnRoundAddedEvent(new GameInformationEventArg {Game = Game});
        }

        #endregion
    }
}
