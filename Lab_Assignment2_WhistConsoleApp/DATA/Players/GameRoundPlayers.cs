using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class GameRoundPlayers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameRoundPlayerId { get; set; }

        //Foreign Key 
        public int GamePlayerId { get; set; }


        public int Points { get; set; }

        //Foreign key
        public int GameRoundId { get; set; }

        //Navigation property for GameRound 
        public GameRounds GameRound { get; set; }

        //Navigation Property
        public GamePlayer GamePlayer { get; set; }
    }
}
