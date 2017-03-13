using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class TeamModel
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
       
        public bool TeamHomeStatus { get; set; }
        public int TeamTimeouts { get; set; }
        public int TeamFouls { get; set; }
        public int TeamTurnovers { get; set; }
        public int TeamShotAttempts { get; set; }
        public int TeamShotMakes { get; set; }
        public int TeamScore { get; set; }
        public PlayerModel SingleTeamPlayer { get; set; }
        public List<PlayerModel> TeamPlayers { get; set; }
        

    }
}