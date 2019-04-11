using System;
using System.Collections.Generic;
using System.Text;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.Events
{
    public class GameInformationEventArg : EventArgs
    {
        public Games Game { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }
    }
}
