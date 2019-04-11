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

        public EndGameView(InGameView inGameView)
        {
            InGameView = inGameView;
            InGameView.EndGameEvent += HandleEndGameEvent;
        }

        #endregion

        #region properties

        public event EventHandler GameEndedEvent;
        public InGameView InGameView { get; set; }
        private DataContext _db;

        #endregion

        #region EventHandlers

        protected virtual void OnGameEndedevent(EventArgs e)
        {
            GameEndedEvent?.Invoke(this, e);
        }

        private void HandleEndGameEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Game ended");
            Console.WriteLine("No winners");

            // Raise game ended event, back to startpage
            OnGameEndedevent(new EventArgs());
        }
        
        #endregion
    }
}
