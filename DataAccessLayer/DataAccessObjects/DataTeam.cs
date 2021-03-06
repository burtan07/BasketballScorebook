﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessObjects
{
    public class DataTeam
    {
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
        public DataPlayer SingleTeamPlayer { get; set; }
        public List<DataPlayer> TeamPlayers { get; set; }
    }
}
