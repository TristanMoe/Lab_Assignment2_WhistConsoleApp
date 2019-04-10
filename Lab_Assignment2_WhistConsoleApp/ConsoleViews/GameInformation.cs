using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistConsoleApp.Repositories;

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
        private string Gamename { get; set; }
        private string[] Firstnames { get; set; }
        private string[] Lastnames { get; set; }

        protected virtual void OnGameCreated(GameInformationEventArg e)
        {
            EventHandler handler = GameCreated;
            handler?.Invoke(this, e);
        }


        public GameInformation(StartPageView viewSubscribe)
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
                try
                {
                    Console.Clear();
                    Console.Write("Please Enter Your Game Name: ");
                    var gamename = Console.ReadLine();
                    Console.WriteLine("\n Please Enter All Four Players Names:");
                    for (int i = 0; i <= 4; i++)
                    {
                        Console.WriteLine($"Player {i}: ");
                        Console.Write("Firstname: ");
                        Firstnames[i] = Console.ReadLine();
                        Console.Write("\n Lastname: ");
                        Lastnames[i] = Console.ReadLine();
                    }

                    Console.WriteLine("\n Press Enter To Start Game");

                    var input = Console.ReadKey(true).Key;

                    if (input == ConsoleKey.Enter)
                    {
                        RepoGame.RepoCreateANewGame(Gamename, Firstnames, Lastnames);
                    }
                    else
                    {
                        throw new Exception("You Must Press Enter To Continue");
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
