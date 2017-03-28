using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessObjects;

namespace BusinessLogicLayer
{
   public  class TeamBusinessLogic
    {

        static TeamDataAccess _teamDAL = new TeamDataAccess();

        public void CreateTeam(LogicTeam boTeam)
        {
            DataTeam daTeam = Map(boTeam);
            _teamDAL.CreateTeam(daTeam);
        }

        public List<LogicTeam> ReadTeams()
        {
            List<DataTeam> daTeamList = _teamDAL.ReadTeams();
            List<LogicTeam> boTeamList = ListMap(daTeamList);

            return boTeamList;
        }


        public void UpdateTeamNameByTeamID(LogicTeam boUpdatedTeam)
        {
            DataTeam daUpdatedTeam = Map(boUpdatedTeam);
            _teamDAL.UpdateTeamName(daUpdatedTeam);
        }



        public void ReadTeamStatsByTeamID(LogicTeam boTeamStats)
        {
            //TO DO: separate this method to send down TeamID, store returned TeamStats, send return TeamStats to Update Method
            List<DataTeam> daTeamStats = _teamDAL.ReadTeamStatsByTeamID(boTeamStats.TeamID);
            LogicTeam boStoredTeamStats = StatListMap(daTeamStats);

            LogicTeam updatedTeamStats = boStoredTeamStats;
            updatedTeamStats.TeamFouls += boTeamStats.TeamFouls;
            updatedTeamStats.TeamTurnovers += boTeamStats.TeamTurnovers;
            updatedTeamStats.TeamShotAttempts += boTeamStats.TeamShotAttempts;
            updatedTeamStats.TeamShotMakes += boTeamStats.TeamShotMakes;
            updatedTeamStats.TeamScore += boTeamStats.TeamScore;

            DataTeam daUpdatedStats = Map(updatedTeamStats);
            _teamDAL.UpdateTeamStatsByTeamID(daUpdatedStats);

        }

                

        static List<LogicTeam> ListMap(List<DataTeam> daTeams)
        {
            List<LogicTeam> boTeams = new List<LogicTeam>();
            foreach (DataTeam dTeam in daTeams)
            {
                LogicTeam lTeam = new LogicTeam();
                lTeam.TeamID = dTeam.TeamID;
                lTeam.TeamName = dTeam.TeamName;
               
                boTeams.Add(lTeam);
            }

            return boTeams;
        }
        static DataTeam Map(LogicTeam boTeam)  //Maps from BO to DA
        {
            DataTeam daTeam = new DataTeam();

            var type_boTeam = boTeam.GetType();
            var type_daTeam = daTeam.GetType();

            foreach(var field_boTeam in type_boTeam.GetFields())
            {
                var field_daTeam = type_daTeam.GetField(field_boTeam.Name);
                field_daTeam.SetValue(daTeam, field_boTeam.GetValue(boTeam));
            }

            foreach(var prop_boTeam in type_boTeam.GetProperties())
            {
                var prop_daTeam = type_daTeam.GetProperty(prop_boTeam.Name);
                prop_daTeam.SetValue(daTeam, prop_boTeam.GetValue(boTeam));
            }
            return daTeam;

        }

        static LogicTeam StatListMap(List<DataTeam> daTeams)
        {
            LogicTeam boTeamStats = new LogicTeam();

            foreach (DataTeam dTeamStat in daTeams)
            {
                var type_daTeamStat = dTeamStat.GetType();
                var type_boTeamStat = boTeamStats.GetType();

                foreach(var field_daTeamStat in type_daTeamStat.GetFields())
                {
                    var field_boTeamStat = type_boTeamStat.GetField(field_daTeamStat.Name);
                    field_boTeamStat.SetValue(boTeamStats, field_daTeamStat.GetValue(dTeamStat));
                }

                foreach (var prop_daTeamStat in type_daTeamStat.GetProperties())
                {
                    var prop_boTeamStat = type_boTeamStat.GetProperty(prop_daTeamStat.Name);
                    prop_boTeamStat.SetValue(boTeamStats, prop_daTeamStat.GetValue(dTeamStat));
                }
               
            }

            return boTeamStats;
        }
    }
}
