using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            SingleGame = new GameModel();
            GamesList = new List<GameModel>();
        }
        public GameModel SingleGame { get; set; }
        public List<GameModel> GamesList { get; set; }
        public List<TeamModel> TeamsList { get; set; }
    }
}