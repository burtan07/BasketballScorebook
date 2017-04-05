using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class PlayerModel
    {
        public int TeamID { get; set; }

        public int PlayerID { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [RegularExpression("^[:alpha:]*$", ErrorMessage = "Please input letters")]
        public string TeamName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression("^[:alpha::punct:'.-]*$", ErrorMessage = "Please input letters or punctuation")]
        public string PlayerLastName { get; set; }

       
        public string PlayerFirstInitial { get; set; }
        public string PlayerRole { get; set; }

        
        public int JerseyNum { get; set; }

      
        public int PlayerAssists { get; set; }

        
        public int PlayerFouls { get; set; }

      
        public int PlayerPoints { get; set; }

        
        public int QuarterPlayed { get; set; }

       
        public int PlayerShotAttempts { get; set; }

       
        public int PlayerShotMakes { get; set; }
    }
}