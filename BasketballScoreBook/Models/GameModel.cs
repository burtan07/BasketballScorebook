using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class GameModel
    {
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamScore { get; set; }

        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamScore { get; set; }

        
    }
}