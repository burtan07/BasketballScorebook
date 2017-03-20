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
    public class PlayerController : Controller
    {
        static PlayerBusinessLogic _playerBLL = new PlayerBusinessLogic();
        static TeamBusinessLogic _teamBLL = new TeamBusinessLogic();
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePlayer()
        {
            PlayerViewModel playerVM = new PlayerViewModel();

            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            List<TeamModel> TeamsList = Map(boTeamList);
            playerVM.TeamsList = new List<TeamModel>();

            ViewBag.TeamsList = TeamsList;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlayer(PlayerViewModel playerVM) //Displays List of Teams using ViewBag & Creates Player with TeamID = TeamNameSelected
        {
            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            List<TeamModel> TeamsList = Map(boTeamList);
            ViewBag.TeamsList = TeamsList;

            LogicPlayer boPlayer = Map(playerVM);
            _playerBLL.CreatePlayer(boPlayer);

            return View();
        }


        [HttpGet]
        public ActionResult ViewPlayers() //Views Current List of Players
        {
            PlayerViewModel playerVM = new PlayerViewModel();

            List<LogicPlayer> boPlayersList = _playerBLL.ViewPlayers();
            playerVM.Players = ListMap(boPlayersList);

            return View(playerVM);
        }


        [HttpGet]
        public ActionResult UpdatePlayer(int PlayerID)  
        {
            //Available to PowerUser from ViewPlayers View Table |  
            //Pulls the selected Players ID that needs updated , checks if it's equal to PlayersList.PLayers.PLayerID

            PlayerViewModel playerVM = new PlayerViewModel();
            PlayerViewModel playerToUpdate = new PlayerViewModel();

            List<LogicPlayer> boPlayersList = _playerBLL.ViewPlayers();
            playerVM.Players = ListMap(boPlayersList);

            foreach (PlayerModel player in playerVM.Players)
            {
                if (PlayerID == player.PlayerID)
                {
                    playerToUpdate.SinglePlayer = player;
                    playerToUpdate.SinglePlayer.PlayerID = PlayerID;
                }
            }

            PlayerViewModel playerTeamsVM = new PlayerViewModel();

            List<LogicTeam> boTeamList = _teamBLL.ReadTeams();
            List<TeamModel> TeamsList = Map(boTeamList);
            playerTeamsVM.TeamsList = new List<TeamModel>();

            ViewBag.TeamsList = TeamsList;

            return View("UpdatePlayer", playerToUpdate);
        }

        [HttpPost]
        public ActionResult UpdatePlayer(PlayerViewModel updatedPlayerVM)
        {
            //Maps & sends Updated Player to BLL

            LogicPlayer boUpdatedPlayer = Map(updatedPlayerVM);
            _playerBLL.UpdatePlayerByPlayerID(boUpdatedPlayer);

            return RedirectToAction("ViewPlayers", "Player");
        }


        [HttpGet]
        public ActionResult DeletePlayer(int PlayerID) 
        {
            // Pulls Player to Deletes ID and sends down to BLL
            _playerBLL.DeletePlayerByPlayerID(PlayerID);

            return RedirectToAction("ViewPlayers", "Player");
        }


        [HttpGet]
        public ActionResult ViewTeamPlayers(int TeamID) //Views Current List of Players on a selected team
        {
            PlayerViewModel teamPlayersVM = new PlayerViewModel();

            List<LogicPlayer> boPlayersList = _playerBLL.ViewTeamPlayers(TeamID);
            teamPlayersVM.Players = ListMap(boPlayersList);

            return View(teamPlayersVM);
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
        static LogicPlayer Map(PlayerViewModel playerVM)  //Maps userVM to boUser
        {
            LogicPlayer boPlayer = new LogicPlayer();

            var type_playerVM = playerVM.SinglePlayer.GetType();
            var type_boPlayer = boPlayer.GetType();

            foreach (var field_playerVM in type_playerVM.GetFields())
            {
                var field_boPlayer = type_boPlayer.GetField(field_playerVM.Name);
                field_boPlayer.SetValue(boPlayer, field_playerVM.GetValue(playerVM.SinglePlayer));
            }

            foreach (var prop_playerVM in type_playerVM.GetProperties())
            {
                var prop_boPlayer = type_boPlayer.GetProperty(prop_playerVM.Name);
                prop_boPlayer.SetValue(boPlayer, prop_playerVM.GetValue(playerVM.SinglePlayer));
            }
            return boPlayer;
        }

        static List<PlayerModel> ListMap(List<LogicPlayer> boPlayersList)
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