using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class GameRoundPlayers
    {
        //Foreign Key 
        public int PlayerPosition { get; set; }
        public int Points { get; set; }

        //Foreign key
        public int GameRoundsId { get; set; }

        //Navigation property for GameRound 
        public GameRounds GameRound { get; set; }

        //Navigation Property
        public GamePlayers GamePlayer { get; set; }
    }
}
