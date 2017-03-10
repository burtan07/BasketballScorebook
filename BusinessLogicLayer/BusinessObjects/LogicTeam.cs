using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessObjects
{
   public class LogicTeam
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamLevel { get; set; }
        public string TeamGender { get; set; }
        public bool TeamHomeStatus { get; set; }
        public int TeamTimeouts { get; set; }
        public int TeamFouls { get; set; }
        public int TeamTurnovers { get; set; }
        public int TeamShotAttempts { get; set; }
        public int TeamShotMakes { get; set; }
        public int TeamScore { get; set; }
        public LogicPlayer SingleTeamPlayer { get; set; }
        public List<LogicPlayer> TeamPlayers { get; set; }
        public List<LogicTeam> TeamList { get; set; }
    }
}
