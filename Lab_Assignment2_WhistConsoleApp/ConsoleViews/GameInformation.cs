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
        private string Gamename { get; set; }
        private string Location { get; set; }
        private string[] Firstnames { get; set; }
        private string[] Lastnames { get; set; }
        public event EventHandler<GameInformationEventArg> GameCreated;

        protected virtual void OnGameCreated(GameInformationEventArg e)
        {
            EventHandler<GameInformationEventArg> handler = GameCreated;
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
                    Gamename = Console.ReadLine();

                    Console.Write("Please Enter Location: ");
                    Location = Console.ReadLine();

                    Console.WriteLine("Please Enter All Four Players Names:");
                    for (int i = 0; i <= 4; i++)
                    {
                        Console.WriteLine($"Player {i}: ");
                        Console.Write("Firstname: ");
                        Firstnames[i] = Console.ReadLine();
                        Console.Write("\n Lastname: ");
                        Lastnames[i] = Console.ReadLine();
                    }
                    
                    Console.WriteLine("Press Enter To Start Game");

                    var input = Console.ReadKey(true).Key;

                    if (input == ConsoleKey.Enter)
                    {
                        RepoGame.RepoCreateANewGame(Gamename, Firstnames, Lastnames, Location);
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
