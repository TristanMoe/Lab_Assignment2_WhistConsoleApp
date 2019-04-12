using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Prints the game chosen 
    /// </summary>
    public class PrintGameView
    {
        private GameIndexView GameIndexView { get; set; }
        private event EventHandler NavigateBack;

        public PrintGameView(GameIndexView gameIndexView)
        {
            GameIndexView = gameIndexView;
            GameIndexView.PrintGameInformation += PrintGameHandler;
            NavigateBack += GameIndexView.HandlePreviousGameEvent;
        }

        public void PrintGameHandler(object sender, GameEventArgs e)
        {
            while (true)
            {
                Console.Clear();

                PrintGame(e.Game);

                Console.Write("Press B to go back: ");
                var input = Console.ReadKey(true).Key;

                try
                {
                    switch (input)
                    {
                        case ConsoleKey.B:
                            NavigateBack?.Invoke(this,EventArgs.Empty);
                            return;
                        default:
                            throw new InputException("Invalid Input!");
                    }
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(1000);
                }
            }
        }

        private void PrintGame(Games game)
        {
            Console.WriteLine($"{game.Name}:");
            Console.WriteLine($"Location: {game.Location.Name}");
            Console.WriteLine($"Last Updated: {game.Updated}");
            Console.WriteLine("Rounds:");
            foreach (var gameGameRound in game.GameRounds)
            {
                Console.WriteLine($"********Round {gameGameRound.RoundNumber}**********");
                Console.WriteLine($"DealerPosition: {gameGameRound.DealerPosition}");
                Console.WriteLine($"Players:");
                foreach (var gameRoundPlayer in gameGameRound.GRPs)
                {
                    Console.WriteLine($"\t{gameRoundPlayer.GamePlayer.Player.FirstName} {gameRoundPlayer.GamePlayer.Player.LastName}: {gameRoundPlayer.Points} pts");
                }
                Console.WriteLine("************************");
            }

        }
    }
}

