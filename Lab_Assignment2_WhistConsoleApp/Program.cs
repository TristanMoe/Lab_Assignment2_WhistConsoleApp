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
                .EnableSensitiveDataLogging()
                .Options;
            var db = new DataContext(options);

            var StartGameView = new StartPageView(); 
            var GameInformationView = new GameInformation(StartGameView, db);
            var InGameView = new InGameView(GameInformationView);
            var GameIndexView = new GameIndexView(StartGameView, db);
            var printGameView = new PrintGameView(GameIndexView);

            StartGameView.StartGame();

            Console.ReadKey();

        }
    }
}
