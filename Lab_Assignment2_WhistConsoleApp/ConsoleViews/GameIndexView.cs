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
            startPageView.FindPreviousGame += HandlePreviousGameEvent; 
        }

        public void HandlePreviousGameEvent(object sender, EventArgs e)
        {
            while (true)
            {
                Console.Clear();

                PrintGames();
                Console.WriteLine("Please Enter The Name Of A Game You Wish To Inspect: ");
                Console.WriteLine("Press B To Go Back");
                Console.Write("Game: ");
                var input = Console.ReadLine();

                try
                {
                    if (input?.ToLower() == "b")
                        StartPageView.StartGame();

                    var game = Repo.GetGame(input);
                    var eventArgs = new GameEventArgs(){Game = game};
                    PrintGameInformation?.Invoke(this, eventArgs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    return;
                }
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
