using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class Rounds
    {
        [Required]
        //Foreign key for Gamerounds
        public int GameRoundsId { get; set; }

        //Navigation Property 
        public GameRounds Gameround { get; set; }

        //Attributes 
        [Range(1, 13)]
        public int Tricks { get; set; }
        [Range(1, 7)]
        public int TricksWon { get; set; }

        public string Trump { get; set; }

        [Required]
        //Foreign key for bid winner position 
        public int BidWinnerPositionId { get; set; }
        //Navigation property 
        public GamePlayers BidWinnerGameplayer { get; set; }

        [Required]
        //Foreign key for bid winner mate position 
        public int BidWinnerMatePositionId { get; set; }
        //Navigation property 
        public GamePlayers BidWinnerMateGameplayer { get; set; }
    }
}
