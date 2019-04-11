using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

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
        private string[] Firstnames { get; set; } = new string[4];
        private string[] Lastnames { get; set; } = new string[4];
        private StartPageView StartPage { get; set; }
        private RepoGame Repo { get; set; }
        


        public event EventHandler<GameInformationEventArg> GameCreated;

        protected virtual void OnGameCreated(GameInformationEventArg e)
        {
            EventHandler<GameInformationEventArg> handler = GameCreated;
            handler?.Invoke(this, e);
        }


        public GameInformation(StartPageView startPageSubscribe, DataContext db)
        {
            Repo = new RepoGame(db);
            StartPage = startPageSubscribe;
            StartPage.GameHasStarted += HandleGameStarted;
        }

        public void HandleGameStarted(object sender, EventArgs e)
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
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Player {i+1}: ");
                        Console.Write("Firstname: ");
                        Firstnames[i] = Console.ReadLine();
                        Console.Write("Lastname: ");
                        Lastnames[i] = Console.ReadLine();
                        Console.WriteLine();
                    }
                    
                    Console.WriteLine("Press Enter To Start Game");

                    var input = Console.ReadKey(true).Key;

                    if (input == ConsoleKey.Enter)
                    {
                        //Create game and get event container 
                        var eventArg = Repo.RepoCreateANewGame(Gamename, Firstnames, Lastnames, Location);
                        //Raise event
                        OnGameCreated(eventArg);
                        return; 
                    }
                    else
                    {
                        throw new Exception("You Must Press Enter To Continue");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return; 
                }

            }

        }
    }
}
