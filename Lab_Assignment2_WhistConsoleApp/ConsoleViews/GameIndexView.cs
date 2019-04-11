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
            Console.Write("Please Enter The Name Of A Game You Wish To Inspect: ");
            var input = Console.ReadLine();

            try
            {
                var game = Repo.GetGame(input);
                var eventArgs = new GameEventArgs(){Game = game};
                PrintGameInformation?.Invoke(this, eventArgs);
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
            }
        }

        public void PrintGames()
        {
            try
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
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
