using System;
using Lab_Assignment2_WhistConsoleApp.ConsoleViews;

namespace Lab_Assignment2_WhistConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var StartGameView = new StartPageView(); 
            StartGameView.StartGame();

            Console.ReadKey();

        }
    }
}
