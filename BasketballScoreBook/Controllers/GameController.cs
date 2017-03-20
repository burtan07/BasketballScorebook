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
    public class GameController : Controller
    {
        static GameBusinessLogic _gameBLL = new GameBusinessLogic();
        static TeamBusinessLogic _teamBLL = new TeamBusinessLogic();

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateGame()
        {
            GameViewModel gameVM = new GameViewModel();

            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            List<GameModel> teamsList = new List<GameModel>();
            gameVM.TeamsList = new List<TeamModel>();

            ViewBag.TeamsList = teamsList;
            return View();
        }

        [HttpPost]
        public ActionResult CreateGame(GameViewModel gameVM)
        {
            LogicGame boGame = Map(gameVM);
            _gameBLL.CreateGame(boGame);

            return View();
        }




        static LogicGame Map(GameViewModel gameVM)
        {
            LogicGame boGame = new LogicGame();

            var type_gameVM = gameVM.SingleGame.GetType();
            var type_boGame = boGame.GetType();

            foreach(var field_gameVM in type_gameVM.GetFields())
            {
                var field_boGame = type_boGame.GetField(field_gameVM.Name);
                field_boGame.SetValue(boGame, field_gameVM.GetValue(gameVM.SingleGame));
            }

            foreach(var prop_gameVM in type_gameVM.GetProperties())
            {
                var prop_boGame = type_boGame.GetProperty(prop_gameVM.Name);
                prop_boGame.SetValue(boGame, prop_gameVM.GetValue(gameVM.SingleGame));
            }
            return boGame;
        }

        static List<TeamModel> Map(List<LogicTeam> boTeams) //Pulls the list of teams from DB to Display in the CreatePlayer View
        {
            List<TeamModel> playerTeams = new List<TeamModel>();
            foreach (LogicTeam lTeam in boTeams)
            {
                TeamModel team = new TeamModel();
                team.TeamID = lTeam.TeamID;
                team.TeamName = lTeam.TeamName;

                playerTeams.Add(team);
            }
            return playerTeams;
        }
    }
}