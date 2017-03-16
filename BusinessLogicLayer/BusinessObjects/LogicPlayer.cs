using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessObjects
{
    public class LogicPlayer
    {
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        
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
