using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class PlayerViewModel
    {
        public PlayerViewModel ()
        {
            SinglePlayer = new PlayerModel();
            Players = new List<PlayerModel>();
            TeamsList = new List<TeamModel>();

        }
        public PlayerModel SinglePlayer { get; set; }
        public List<PlayerModel> Players { get; set; }

        public List<TeamModel> TeamsList { get; set; }
    }
}