﻿using System;
using Lab_Assignment2_WhistConsoleApp.ConsoleViews;
using Lab_Assignment2_WhistConsoleApp.Repositories;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Whist Database, Please Wait ...");

            var options = new DbContextOptionsBuilder<DataContext>()
                .EnableSensitiveDataLogging()
                .Options;
            var db = new DataContext();
            db.SeedData();
            var repo = new RepoGame(db);
            var StartGameView = new StartPageView(); 
            var GameInformationView = new GameInformation(StartGameView, db);
            var InGameView = new InGameView(GameInformationView);
            var AddRoundView = new AddRound(InGameView, repo);
            var GameIndexView = new GameIndexView(StartGameView, db);
            var EndGameView = new EndGameView(InGameView, StartGameView);
            var printGameView = new PrintGameView(GameIndexView);
            var WinnerGameView = new WinnerView(AddRoundView, StartGameView);
            
            StartGameView.StartGame();

            Console.ReadKey();

        }
    }
}
