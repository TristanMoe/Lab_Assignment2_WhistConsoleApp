using System;
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

        public InGameView InGameView { get; set; }
        public Games Game { get; set; }
        public string Trump { get; set; }
        public List<GamePlayers> GamePlayers { get; set; }
        private DataContext _db;

        #endregion

        #region Constructor

        public AddRound(InGameView inGameView, DataContext db)
        {
            InGameView = inGameView;
            _db = db;
            GamePlayers = new List<GamePlayers>();
            // eventhandler
        }

        #endregion

        #region EventHandlers

        private void HandleRoundAddedEvent(object sender, GameInformationEventArg e)
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

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            // Raise ingame event
        }

        #endregion
    }
}
