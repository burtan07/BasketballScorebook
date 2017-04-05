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

        
        public int HomeTeamScore { get; set; }

      
        public int HomeTeamFouls { get; set; }

       
        public int HomeTeamTimeOuts { get; set; }

        
        public int HomeTeamTurnOvers { get; set; }

        
        public int HomeTeamShotAttempts { get; set; }

        
        public int HomeTeamShotMakes { get; set; }

        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }

       
        public int AwayTeamScore { get; set; }


       
        public int AwayTeamFouls { get; set; }

       
        public int AwayTeamTimeOuts { get; set; }

        
        public int AwayTeamTurnOvers { get; set; }

        
        public int AwayTeamShotAttempts { get; set; }

       
        public int AwayTeamShotMakes { get; set; }


    }
}