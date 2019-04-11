using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Game ended with no winner
    /// Press enter to proceed 
    /// </summary>
    public class EndGameView
    {
        #region Constructor

        public EndGameView(InGameView inGameView, DataContext db)
        {
            InGameView = inGameView;
            _db = db;
            InGameView.EndGameEvent += HandleEndGameEvent;
        }

        #endregion

        #region properties

        public InGameView InGameView { get; set; }
        private DataContext _db;

        #endregion

        #region EventHandlers

        private void HandleEndGameEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Game ended");
            Console.WriteLine("No winners");

            // Raise game ended event, back to startpage
            
        }
        
        #endregion
    }
}
