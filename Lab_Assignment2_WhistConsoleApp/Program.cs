using System;
using Lab_Assignment2_WhistConsoleApp.ConsoleViews;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            var db = new DataContext(options);
            db.SeedData();

            var startGameView = new StartPageView(); 
            var gameInformationView = new GameInformation(startGameView, db);
            var inGameView = new InGameView(gameInformationView);
            var AddRoundView = new AddRound(inGameView, db);
            var gameIndexView = new GameIndexView(startGameView, db);
            var printGameView = new PrintGameView(gameIndexView);

            var winGameView = new WinnerView(AddRoundView, db);
            var endGameView = new EndGameView(inGameView);

            startGameView.SubscribeToEvents(endGameView, winGameView);
            
            startGameView.StartGame();

            Console.ReadKey();

        }
    }
}
