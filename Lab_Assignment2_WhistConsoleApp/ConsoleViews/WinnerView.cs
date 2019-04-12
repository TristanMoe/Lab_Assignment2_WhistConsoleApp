using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Player X and Player Y won the game
    /// Press enter to proceed 
    /// </summary>
    public class WinnerView
    {
        private readonly StartPageView _startPage;

        #region Constructor

        public WinnerView(AddRound addRound, StartPageView startPage)
        {
            _startPage = startPage;
            AddRound = addRound;
            AddRound.WinnerFoundEvent += HandleWinnerFoundEvent;
        }

        #endregion

        #region Properties
        public AddRound AddRound { get; set; }
        public Team WinnerTeam { get; set; }

        #endregion

        #region EventHandlers

        private void HandleWinnerFoundEvent(object sender, WinnerInformationEventArgs e)
        {
            Console.Clear();
            
            // Check received information
            try
            {
                if (e.WinnerTeam == null)
                    throw new ArgumentNullException("No winner team received!");

                WinnerTeam = e.WinnerTeam;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"{WinnerTeam.Name} has won the game");
            foreach (var player in WinnerTeam.GamePlayers)
            {
                Console.WriteLine($"Congratulations {player.Player.FirstName} {player.Player.LastName}");
            }

            Console.ReadLine();

            _startPage.StartGame();
        }
        #endregion
    }
}
