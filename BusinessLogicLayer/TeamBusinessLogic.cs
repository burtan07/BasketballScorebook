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


        //public void DeleteTeamByTeamID(int boTeamIDtoDEL)
        //{
          
        //    _teamDAL.DeleteTeamByTeamID(boTeamIDtoDEL);
        //}

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
    }
}
