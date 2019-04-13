using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        public List<GameRoundPlayers> PlayerGameRoundPlayers { get; set; }=new List<GameRoundPlayers>();
        public List<GameRoundPlayers> CurrentGameRoundPlayerses { get; set; }=new List<GameRoundPlayers>();

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
                    int i = 0;
                    int points = 0;
                    while (true)
                    {
                        if(i>0)
                            Console.WriteLine("Point has to be between 0 and 13. Please try again.");
                        // Update points for the gameplayer
                        string result = Console.ReadLine();

                       

                        if (!string.IsNullOrEmpty(result) && -1<points && points<14)
                            points = int.Parse(result);
                        else
                        {
                            i++;
                            continue;
                        }

                        break;
                    }

                    // update points for gameplayer's team
                    
                    player.Teams.Points +=points;

                        
                    
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
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            foreach (var team in Game.Teams)
            {
                if (team.Points > 6)
                    team.Points -= 6;
                else
                {
                    team.Points = 0;
                }
                if (team.Points >= 5)
                {
                    OnWinnerFoundEvent(new WinnerInformationEventArgs { WinnerTeam = team });
                    return;
                }
            }
            _repoGame._db.SaveChanges();
            // Back to InGameView
            OnRoundAddedEvent(new GameInformationEventArg {Game = Game});
        }

        #endregion
    }
}
