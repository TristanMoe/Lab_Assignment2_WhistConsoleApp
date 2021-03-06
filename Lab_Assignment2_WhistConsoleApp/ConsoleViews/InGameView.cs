﻿using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// 1. AddRound
    /// 2. End Game 
    /// </summary>
    public class InGameView
    {
        #region Constructor

        public InGameView(GameInformation gameInformation)
        {
            GameInformation = gameInformation;
            GameInformation.GameCreated += HandleInGameEvents;
        }

        #endregion

        #region Properties

        public event EventHandler EndGameEvent;
        public event EventHandler<GameInformationEventArg> AddRoundEvent;
        public GameInformation GameInformation { get; set; }
        public AddRound AddRound { get; set; }
        public Games Game { get; set; }

        #endregion

        #region Eventhandlers

        protected virtual void OnAddRoundevent(GameInformationEventArg e)
        {
            AddRoundEvent?.Invoke(this, e);
        }

        protected virtual void OnEndGameevent(EventArgs e)
        {
            EndGameEvent?.Invoke(this, e);
        }

        public void HandleInGameEvents(object sender, GameInformationEventArg e)
        {
            Console.Clear();
            try
            {
                if (e.Game == null)
                    throw new ArgumentNullException("No gameinformation received!");

                Game = e.Game;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Add round");
            Console.WriteLine("2. End game");
            while (true)
            {
                try
                {
                    var input = Console.ReadKey(true).Key;
                    if (input == ConsoleKey.D1)
                    {
                        // Raising round added event
                        OnAddRoundevent(new GameInformationEventArg {Game = Game});
                        return;
                    }

                    if (input == ConsoleKey.D2)
                    {
                        // Raising end game event
                        OnEndGameevent(EventArgs.Empty);
                        return;
                    }

                    throw new InputException("Must choose options 1 or 2, try again");
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //    Console.ReadLine();
                //    return;
                //}
            }
        }
        #endregion
    }
}
