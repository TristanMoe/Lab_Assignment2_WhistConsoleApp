using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Prints the start view
    /// 1. Startgame
    /// 2. Choose Game 
    /// </summary>
    public class StartPageView
    {
        public event EventHandler GameHasStarted;
        public event EventHandler FindPreviousGame;

        protected virtual void OnFindPreviousGame(EventArgs e)
        {
            EventHandler handler = FindPreviousGame;
            handler?.Invoke(this, e);
        }


        protected virtual void OnGameHasStarted(EventArgs e)
        {
            EventHandler handler = GameHasStarted;
            handler?.Invoke(this, e);
        }

        public void StartGame()
        {
            while (true)
            { 


                Console.Clear();
                Console.WriteLine("Welcome to Whist Point Calculator");
                Console.WriteLine("Use keys to navigation through the console");
                Console.WriteLine("1: Create New Game");
                Console.WriteLine("2: Find Previous Games");

                var input = Console.ReadKey(true).Key;

                try
                {
                    switch (input)
                    {
                        case ConsoleKey.D1:
                            OnGameHasStarted(EventArgs.Empty);
                            return; 
                        case ConsoleKey.D2:
                            OnFindPreviousGame(EventArgs.Empty);
                            return; 
                        default: throw new Exception("Invalid input!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
