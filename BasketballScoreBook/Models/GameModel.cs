using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BasketballScoreBook.Models
{
    public class GameModel
    {
        public int GameID { get; set; }
        public int HomeTeamID { get; set; }

        public string HomeTeamName { get; set; }

        [Display(Name = "Score")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamScore { get; set; }

        [Display(Name = "Team Fouls:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamFouls { get; set; }

        [Display(Name = "Timeouts:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamTimeOuts { get; set; }

        [Display(Name = "Turnovers:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamTurnOvers { get; set; }

        [Display(Name = "Shot Attempts:")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamShotAttempts { get; set; }

        [Display(Name = "Shots Made:")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int HomeTeamShotMakes { get; set; }

        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }

        [Display(Name = "Score")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamScore { get; set; }


        [Display(Name = "Team Fouls:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamFouls { get; set; }

        [Display(Name = "Timeouts:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamTimeOuts { get; set; }

        [Display(Name = "Turnovers:")]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamTurnOvers { get; set; }

        [Display(Name = "Shot Attempts:")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamShotAttempts { get; set; }

        [Display(Name = "Shots Made:")]
        [MaxLength(3)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please input numbers")]
        public int AwayTeamShotMakes { get; set; }


    }
}