using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.Events;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    class AddRound
    {
        public event EventHandler<GameInformationEventArg> RoundAdded;

        public InGameView InGameView { get; set; }
        public Games Game { get; set; }
        public List<GamePlayers> GamePlayers { get; set; }

        public AddRound(InGameView inGameView)
        {
            InGameView = inGameView;
            GamePlayers = new List<GamePlayers>();
            // eventhandler
        }
    }
}
