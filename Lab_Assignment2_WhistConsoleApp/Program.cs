using System;
using Lab_Assignment2_WhistConsoleApp.ConsoleViews;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DataContext();

            var StartGameView = new StartPageView(); 
            var GameInformationView = new GameInformation(StartGameView, db);
            var InGameView = new InGameView(GameInformationView);
            var GameIndexView = new GameIndexView(StartGameView, db);

            StartGameView.StartGame();

            Console.ReadKey();

        }
    }
}
