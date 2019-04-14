using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    public class GameEventArgs:EventArgs
    {
        public Games Game { get; set; }
    }
    /// <summary>
    /// List of games
    /// </summary>
    public class GameIndexView
    {
        private RepoGame Repo { get; set; }
        private StartPageView StartPageView { get; set; }
        public event EventHandler<GameEventArgs> PrintGameInformation;
        private event EventHandler NavigateBack;
        public GameIndexView(StartPageView startPageView, DataContext db)
        {
            Repo = new RepoGame(db);
            StartPageView = startPageView;
            StartPageView.FindPreviousGame += HandlePreviousGameEvent; 
        }

        public void HandlePreviousGameEvent(object sender, EventArgs e)
        {
            int i = 0;
            while (true)
            {
                Console.Clear();
                if(i!=0)
                    Console.WriteLine("Game was not found. Please try again with another name");
                PrintGames();
                Console.Write("Please Enter The Name Of A Game You Wish To Inspect or enter to go back: ");
                var input = Console.ReadLine();
                if (input == "")
                {
                    return;
                }
                
                    var game = Repo.GetGame(input);
                    if (game == null)
                    {
                        i++;
                        continue;

                    }
                       
                    var eventArgs = new GameEventArgs(){Game = game};
                    PrintGameInformation?.Invoke(this, eventArgs);
                
            }
        }

        public void PrintGames()
        {
            var games = Repo.GetAllGames();

            foreach (var game in games)
            {
                Console.WriteLine($"GAME: {game.Name}");
                Console.WriteLine($"Players:");

                foreach (var gameplayer in game.GamePlayers)
                {
                    Console.WriteLine(
                        $"Player {gameplayer.PlayerPosition}:{gameplayer.Player.FirstName} {gameplayer.Player.LastName}");
                }
            }
        }
    }
}
