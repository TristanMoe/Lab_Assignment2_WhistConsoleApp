﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Lab_Assignment2_WhistPointCalculator;

namespace Lab_Assignment2_WhistConsoleApp.DATA.Team
{
    public class Team
    {
        public string Name { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeamId { get; set; }
        public int Points { get; set; }

        //Navigation to GamePlayer
        public List<GamePlayer> GamePlayers { get; set; }

        //Foreign key for team
        public long GamesId { get; set; }
        //navigation to games
        public Games Games { get; set; }
        
    }
}
