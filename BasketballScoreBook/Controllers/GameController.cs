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
        static PlayerBusinessLogic _playerBLL = new PlayerBusinessLogic();

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateGame() //Displays list of teams, pulls user selection for Home & Away TeamNames & TeamIDs
        {
            GameViewModel gameVM = new GameViewModel();

            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            List<TeamModel> teamsList = Map(boTeamList);
            gameVM.TeamsList = new List<TeamModel>();

            ViewBag.TeamsList = teamsList;
            return View();
        }

        [HttpPost]
        public ActionResult CreateGame(GameViewModel gameTeams)
        {

            return ScoreGameTeams(gameTeams);
        }



        [HttpGet]
        public ActionResult ScoreGameTeams(GameViewModel gameTeamIDs) //sends down Selected Home & Away TeamIDs, Gets Player Lists for those Teams, Assigns TeamID & teamNames to GameVM
        {
           GameViewModel gameTeamPlayersList = new GameViewModel();
           
            List<LogicPlayer> boHomeTeam = _playerBLL.ViewTeamPlayers(gameTeamIDs.SingleGame.HomeTeamID);
            gameTeamPlayersList.homeTeamPlayers = PlayerListMap(boHomeTeam);

            PlayerModel singleHomePlayer = gameTeamPlayersList.homeTeamPlayers[0];
            gameTeamPlayersList.SingleGame.HomeTeamID = singleHomePlayer.TeamID;
            gameTeamPlayersList.SingleGame.HomeTeamName = singleHomePlayer.TeamName;

            List<LogicPlayer> boAwayTeam = _playerBLL.ViewTeamPlayers(gameTeamIDs.SingleGame.AwayTeamID);
            gameTeamPlayersList.awayTeamPlayers = PlayerListMap(boAwayTeam);

            PlayerModel singleAwayPlayer = gameTeamPlayersList.awayTeamPlayers[0];
            gameTeamPlayersList.SingleGame.AwayTeamID = singleAwayPlayer.TeamID;
            gameTeamPlayersList.SingleGame.AwayTeamName = singleAwayPlayer.TeamName;

            return View("ScoreGame", gameTeamPlayersList);
        }

        [HttpPost]
        public ActionResult ScoreGame(GameViewModel gameVM)
        {
            LogicGame boGame = Map(gameVM);
            _gameBLL.CreateGame(boGame);

            return RedirectToAction("CreateGame", "Game");
        }




        static LogicGame Map(GameViewModel gameVM)
        {
            LogicGame boGame = new LogicGame();

            var type_gameVM = gameVM.SingleGame.GetType();
            var type_boGame = boGame.GetType();

            foreach (var field_gameVM in type_gameVM.GetFields())
            {
                var field_boGame = type_boGame.GetField(field_gameVM.Name);
                field_boGame.SetValue(boGame, field_gameVM.GetValue(gameVM.SingleGame));
            }

            foreach (var prop_gameVM in type_gameVM.GetProperties())
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

        static List<PlayerModel> PlayerListMap(List<LogicPlayer> boPlayersList)
        {
            //foreach LogicPlayer object in the List, get the data types(classes) for each object,
            //Gets the fields(name & value) foreach Type in boPlayers & assigns to playerVM using SetValue Method,
            //Gets the properties(name & value)foeach Type in boPlayers & assigns to playerVM using SetValue Method
            //then adds the playerVM obj to the List of PLayerModel

            List<PlayerModel> playersList = new List<PlayerModel>();
            foreach (LogicPlayer boPlayer in boPlayersList)
            {
                PlayerModel playerVM = new PlayerModel();

                var type_boPlayer = boPlayer.GetType();
                var type_playerVM = playerVM.GetType();

                foreach (var field_boPlayer in type_boPlayer.GetFields())
                {
                    var field_playerVM = type_playerVM.GetField(field_boPlayer.Name);
                    field_playerVM.SetValue(playerVM, field_boPlayer.GetValue(boPlayer));
                }

                foreach (var prop_boPlayer in type_boPlayer.GetProperties())
                {
                    var prop_playerVM = type_playerVM.GetProperty(prop_boPlayer.Name);
                    prop_playerVM.SetValue(playerVM, prop_boPlayer.GetValue(boPlayer));
                }
                playersList.Add(playerVM);
            }
            return playersList;
        }
    }
}