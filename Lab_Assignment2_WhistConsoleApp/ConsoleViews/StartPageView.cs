﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// Prints the start view
    /// 1. Startgame
    /// 2. Choose Game 
    /// </summary>
    public class StartPageView
    {
        public event EventHandler GameHasStarted;
        public event EventHandler FindPreviousGame;
        public EndGameView EndGameView { get; set; }
        public WinnerView WinnerView { get; set; }

      
        public StartPageView()
        {
            Console.WriteLine("Loading Whist Database...");
            
        }

        protected virtual void OnFindPreviousGame(EventArgs e)
        {
            EventHandler handler = FindPreviousGame;
            handler?.Invoke(this, e);
        }

        protected virtual void OnGameHasStarted(EventArgs e)
        {
            EventHandler handler = GameHasStarted;
            handler?.Invoke(this, e);
        }

        public void StartGame()
        {
            while (true)
            { 
                Console.Clear();
                Console.WriteLine("Welcome To Whist Point Calculator");
                Console.WriteLine("Use Keys To Navigation Through The Console");
                Console.WriteLine("1: Create New Game");
                Console.WriteLine("2: Find Previous Games");

                var input = Console.ReadKey(true).Key;

                try
                {
                    switch (input)
                    {
                        case ConsoleKey.D1:
                            OnGameHasStarted(EventArgs.Empty);
                            break; 
                        case ConsoleKey.D2:
                            OnFindPreviousGame(EventArgs.Empty);
                            break; 
                        default:
                            throw new InputException("Invalid Input!");
                    }
                }
                catch (InputException e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
