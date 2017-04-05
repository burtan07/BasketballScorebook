using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class TeamModel
    {
        public int TeamID { get; set; }


        [MaxLength(20)]
        [MinLength(5)]
        [RegularExpression("^[:alpha:]*$", ErrorMessage = "Please input letters")]
        public string TeamName { get; set; }

        public bool TeamHomeStatus { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamTimeouts { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamFouls { get; set; }

        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamTurnovers { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamShotAttempts { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamShotMakes { get; set; }

        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input number")]
        public int TeamScore { get; set; }

    }
}