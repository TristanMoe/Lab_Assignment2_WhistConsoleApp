using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;

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
            GamePlayers = new List<GamePlayer>();
            GameInformation.GameCreated += HandleGameCreatedEvent;
        }

        #endregion

        #region Properties

        public event EventHandler<GameInformationEventArg> RoundAddedEvent;
        public GameInformation GameInformation { get; private set; }
        public Games Game { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }

        #endregion

        #region Eventhandlers

        protected virtual void OnRoundAddedEvent(GameInformationEventArg e)
        {
            RoundAddedEvent?.Invoke(this, e);
        }

        private void HandleGameCreatedEvent(object sender, GameInformationEventArg e)
        {
            Console.Clear();
            try
            {
                if (e.Game == null || e.GamePlayers == null)
                    throw new ArgumentNullException("No gameinformation received!");

                Game = e.Game;
                GamePlayers = e.GamePlayers;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Add round");
            Console.WriteLine("2. End game");
            while (true)
            {
                try
                {
                    string action = Console.ReadLine();
                    if (action.Equals("1"))
                    {
                        // Raising round added event
                        OnRoundAddedEvent(new GameInformationEventArg {Game = Game, GamePlayers = GamePlayers});
                        return;
                    }
                    if (action.Equals("2"))
                    {
                        Console.WriteLine("Game ended");
                        Console.WriteLine("No winner");

                        // Rasing start page event

                        return;
                    }
                    throw new Exception("Must choose options 1 or 2, try again");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        #endregion
    }
}
