using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class Games
    {
        //Attributes

        //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GamesId { get; set; }
        public string Name { get; set; }

        //public string Description { get; set; }

        public bool Started { get; set; }
        public bool Ended { get; set; }
        public DateTime Updated { get; set; }

        //Foreign key for location
        public int LocationId { get; set; }

        //Navigation Property for Location
        public Location Location { get; set; }

        //Navigation Property for Game Round
        public List<GameRounds> GameRounds { get; set; }

        //Navigation Property for Gameplayers
        public List<GamePlayers> GamePlayers { get; set; }
    }
}
