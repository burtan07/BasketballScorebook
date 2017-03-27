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
        public int HomeTeamScore { get; set; }

        [Display(Name = "Team Fouls:")]
        public int HomeTeamFouls { get; set; }

        [Display(Name = "Timeouts:")]
        public int HomeTeamTimeOuts { get; set; }

        [Display(Name = "Turnovers:")]
        public int HomeTeamTurnOvers { get; set; }

        [Display(Name = "Shot Attempts:")]
        public int HomeTeamShotAttempts { get; set; }

        [Display(Name = "Shots Made:")]
        public int HomeTeamShotMakes { get; set; }

        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }

        [Display(Name = "Score")]
        public int AwayTeamScore { get; set; }


        [Display(Name = "Team Fouls:")]
        public int AwayTeamFouls { get; set; }

        [Display(Name = "Timeouts:")]
        public int AwayTeamTimeOuts { get; set; }

        [Display(Name = "Turnovers:")]
        public int AwayTeamTurnOvers { get; set; }

        [Display(Name = "Shot Attempts:")]
        public int AwayTeamShotAttempts { get; set; }

        [Display(Name = "Shots Made:")]
        public int AwayTeamShotMakes { get; set; }


    }
}