using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class GameRounds
    {
        //Attributes

        //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameRoundsId { get; set; }

        [Range(1,9)]
        public int RoundNumber { get; set; }

        //Which players turn it is to deal?
        [Range(1,4)]
        public int DealerPosition { get; set; }
        public bool Ended { get; set; }
        public bool Started { get; set; }

        //Foreign key for Games
        public int GamesId { get; set; }
        //Navigation Property for Games
        public Games Game { get; set; }

        //Foreign Key for roundtype 
        public int RoundTypeId { get; set; }

        //Navigation Property for Rounds
        public List<Rounds> Rounds { get; set; }

        //Navigation Property for GameRoundPlayer
        public List<GameRoundPlayers> GRPs { get; set; }

    }

}
