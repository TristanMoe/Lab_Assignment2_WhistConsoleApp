using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class GamePlayer
    {

        public int PlayerPosition { get; set; }

        //Foreign Key to team
        public int TeamId { get; set; }


        //Primary key for Player 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GamePlayerId { get; set; }


        //Foreign Key for Players
        public int PlayerId { get; set; }

        //Navigation Property for players
        public Players Player { get; set; }





        //Foreign Key for Game
        public int GamesId { get; set; }

        //Navigation Property for Games 
        public Games Game { get; set; }
        

        //Navigation Property for GameRoundPlayer
        public List<GameRoundPlayers> GRPs { get; set; }

        //Team NavigationProperty
        public Team Teams { get; set; }
    }
}
