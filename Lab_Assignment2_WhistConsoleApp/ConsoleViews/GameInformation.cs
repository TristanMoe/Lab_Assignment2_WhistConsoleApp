using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Game information
    /// Enter Game Name:
    /// Name of Players:
    /// Player 1:
    /// Player 2:
    /// Player 3:
    /// Player 4:
    /// Press Enter For Start
    /// </summary>
    public class GameInformation
    {
        public event EventHandler GameCreated;

        protected virtual void OnGameCreated(EventArgs e)
        {
            EventHandler handler = GameCreated;
            handler?.Invoke(this, e);
        }


        public GameInformation(EventHandler eventsub)
        {

        }

        public void GameStarted(object sender, EventArgs e)
        {
            CreateNewGame();
        }
        
        public void CreateNewGame()
        {
            while (true)
            {
                Console.WriteLine("Please Enter Your Game Name: ");
            }

        }
    }
}
