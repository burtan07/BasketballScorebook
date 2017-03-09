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

        static DataTeam Map(LogicTeam boTeam)
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
