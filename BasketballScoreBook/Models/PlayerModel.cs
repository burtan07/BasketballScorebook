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

        [MaxLength(1)]
        [MinLength(1)]
        [RegularExpression("^[:alpha:]$", ErrorMessage = "Please input a letter ")]
        public string PlayerFirstInitial { get; set; }
        public string PlayerRole { get; set; }

        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int JerseyNum { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int PlayerAssists { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int PlayerFouls { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int PlayerPoints { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int QuarterPlayed { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int PlayerShotAttempts { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int PlayerShotMakes { get; set; }
    }
}