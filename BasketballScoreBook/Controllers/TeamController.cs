using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasketballScoreBook.Models;
using BusinessLogicLayer;
using BusinessLogicLayer.BusinessObjects;

namespace BasketballScoreBook.Controllers
{
    public class TeamController : Controller
    {
        static TeamBusinessLogic _teamBLL = new TeamBusinessLogic();
        static PlayerBusinessLogic _playerBLL = new PlayerBusinessLogic();
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeam(TeamViewModel teamVM)
        {
            LogicTeam boTeam = Map(teamVM);
            _teamBLL.CreateTeam(boTeam);

            return RedirectToAction("ViewTeams", "Team"); //redirect to action()
        }

        [HttpGet]
        public ActionResult ViewTeams()
        {
            TeamViewModel teamVM = new TeamViewModel();

            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            teamVM.TeamList = ListMap(boTeamList);

            return View(teamVM);

        }

        [HttpGet]
        public ActionResult UpdateTeamName(int TeamID)
        {
            TeamViewModel teamsListVM = new TeamViewModel();
            TeamViewModel teamNameToUpdate = new TeamViewModel();

            List<LogicTeam> boTeamsList = _teamBLL.ReadTeams();
            teamsListVM.TeamList = ListMap(boTeamsList);

            foreach (TeamModel team in teamsListVM.TeamList)
            {
                if (TeamID == team.TeamID)
                {
                    teamNameToUpdate.SingleTeam = team;
                    teamNameToUpdate.SingleTeam.TeamID = TeamID;
                }
            }

            return View("UpdateTeamName", teamNameToUpdate);
        }


        [HttpPost]
        public ActionResult UpdateTeamName(TeamViewModel updatedTeamVM)
        {
            LogicTeam boUpdatedTeamName = Map(updatedTeamVM);
            _teamBLL.UpdateTeamNameByTeamID(boUpdatedTeamName);

            return RedirectToAction("ViewTeams", "Team");
        }


        [HttpGet]
        public ActionResult DeleteTeam(int TeamID)
        {

            _playerBLL.DeletePlayerByTeamID(TeamID);
           // _teamBLL.DeleteTeamByTeamID(TeamID);

            return RedirectToAction("ViewTeams", "Team");
        }

        [HttpGet]
        public ActionResult ViewTeamStats(int TeamID)
        {
            LogicTeam boTeamStats = _teamBLL.ReadTeamStatsByTeamID(TeamID);
            TeamViewModel teamStats = Map(boTeamStats);
            
            return View(teamStats);
        }

        static LogicTeam Map(TeamViewModel teamVM)
        {
            LogicTeam boTeam = new LogicTeam();

            var type_teamVM = teamVM.SingleTeam.GetType();
            var type_boTeam = boTeam.GetType();

            foreach (var field_teamVM in type_teamVM.GetFields())
            {
                var field_boTeam = type_boTeam.GetField(field_teamVM.Name);
                field_boTeam.SetValue(boTeam, field_teamVM.GetValue(teamVM.SingleTeam));
            }

            foreach (var prop_teamVM in type_teamVM.GetProperties())
            {
                var prop_boTeam = type_boTeam.GetProperty(prop_teamVM.Name);
                prop_boTeam.SetValue(boTeam, prop_teamVM.GetValue(teamVM.SingleTeam));
            }

            return boTeam;
        }

        static List<TeamModel> ListMap(List<LogicTeam> boTeams) //Pulls the list of teams from DB to Display in the CreatePlayer View
        {
            List<TeamModel> teamsList = new List<TeamModel>();
            foreach (LogicTeam lTeam in boTeams)
            {
                TeamModel team = new TeamModel();
                team.TeamID = lTeam.TeamID;
                team.TeamName = lTeam.TeamName;

                teamsList.Add(team);
            }
            return teamsList;
        }

        static TeamViewModel Map(LogicTeam boTeam)
        {
            TeamViewModel teamM = new TeamViewModel();

            var type_boTeam = boTeam.GetType();
            var type_teamM = teamM.SingleTeam.GetType();

            foreach(var field_boTeam in type_boTeam.GetFields())
            {
                var field_teamM = type_teamM.GetField(field_boTeam.Name);
                field_teamM.SetValue(teamM.SingleTeam, field_boTeam.GetValue(boTeam));
            }
            foreach(var prop_boTeam in type_boTeam.GetProperties())
            {
                var prop_teamM = type_teamM.GetProperty(prop_boTeam.Name);
                prop_teamM.SetValue(teamM.SingleTeam, prop_boTeam.GetValue(boTeam));
            }
            return teamM;

        }

    }
}