﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class Players
    {
        //Attributes

        //Primary Key
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlayerId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        //Navigation Property
        public List<GamePlayer> GamePlayers { get; set; }
    }
}
