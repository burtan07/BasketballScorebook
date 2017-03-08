using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class PlayerViewModel
    {
        public PlayerModel SinglePlayer { get; set; }
        public List<PlayerModel> Players { get; set; }
    }
}