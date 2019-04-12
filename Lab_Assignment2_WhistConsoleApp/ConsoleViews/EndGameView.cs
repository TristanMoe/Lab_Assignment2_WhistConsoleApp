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

        public EndGameView(InGameView inGameView, StartPageView startPage)
        {
            InGameView = inGameView;
            InGameView.EndGameEvent += HandleEndGameEvent;
            _startPage = startPage;
        }

        #endregion

        #region properties
        public InGameView InGameView { get; set; }
        private DataContext _db;
        private StartPageView _startPage;

        #endregion

        #region EventHandlers

        private void HandleEndGameEvent(object sender, EventArgs e)
        {
            Console.Clear();

            Console.WriteLine("Game ended");
            Console.WriteLine("No winners");

            Console.ReadLine();

            _startPage.StartGame();
        }
        
        #endregion
    }
}
