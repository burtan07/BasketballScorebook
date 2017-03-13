using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class TeamViewModel
    {
        public TeamModel SingleTeam { get; set;}
        public List<TeamModel> TeamList { get; set; }
        
    }
}