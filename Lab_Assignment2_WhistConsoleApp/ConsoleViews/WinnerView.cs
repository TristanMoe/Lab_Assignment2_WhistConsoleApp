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
        #region Constructor

        public WinnerView(AddRound addRound, DataContext db)
        {
            AddRound = addRound;
            _db = db;
            AddRound.WinnerFoundEvent += HandleWinnerFoundEvent;
        }

        #endregion

        #region Properties

        public event EventHandler WinnerScreenOverEvent;
        private DataContext _db;
        public AddRound AddRound { get; set; }
        public Team WinnerTeam { get; set; }

        #endregion

        #region EventHandlers

        protected virtual void OnWinnerScreenOverevent(EventArgs e)
        {
            WinnerScreenOverEvent?.Invoke(this, e);
        }

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
                return;
            }

            Console.WriteLine($"{WinnerTeam} has won the game");
            foreach (var player in WinnerTeam.GamePlayers)
            {
                Console.WriteLine($"Congratulations {player.Player.FirstName} {player.Player.LastName}");
            }

            Console.ReadLine();

            // Back to startpageview
            OnWinnerScreenOverevent(new EventArgs());
        }
        #endregion
    }
}
