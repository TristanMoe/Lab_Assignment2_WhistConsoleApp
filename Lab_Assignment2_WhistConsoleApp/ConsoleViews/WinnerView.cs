using System;
using System.Collections.Generic;
using System.Text;
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
            GamePlayers = new List<GamePlayer>();
            AddRound.WinnerFoundEvent += HandleWinnerFoundEvent;
        }

        #endregion

        #region Properties

        private DataContext _db;
        public AddRound AddRound { get; set; }
        public Games Game { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }

        #endregion

        #region EventHandlers

        private void HandleWinnerFoundEvent(object sender, GameInformationEventArg e)
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


        }

        #endregion
    }
}
