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
            //gameVM.SingleGame.Team stats gets sent to TeamStats Table
            //gameVM.homeTeamPlayers stats gets sent to Player Table
            LogicTeam boHomeTeamStats = HomeStatsMap(gameVM);
            _teamBLL.UpdateTeamStatsByTeamID(boHomeTeamStats);

            LogicTeam boAwayTeamStats = AwayStatsMap(gameVM);
            _teamBLL.UpdateTeamStatsByTeamID(boAwayTeamStats);

            List<PlayerModel> gameHomePlayers = gameVM.homeTeamPlayers;
            List<LogicPlayer> boHomePlayerStats = PlayerStatsMap(gameHomePlayers);
            _playerBLL.UpdatePlayerStatsByPlayerID(boHomePlayerStats);

            List<PlayerModel> gameAwayPlayers = gameVM.awayTeamPlayers;
            List<LogicPlayer> boAwayPlayerStats = PlayerStatsMap(gameAwayPlayers);
            _playerBLL.UpdatePlayerStatsByPlayerID(boAwayPlayerStats);

            LogicGame boGame = Map(gameVM);
            _gameBLL.CreateGame(boGame);

            return RedirectToAction("CreateGame", "Game");
        }

        public ActionResult PlayerStatPartialView(int ID)
        {
            PlayerViewModel partialPlayer = new PlayerViewModel();
            partialPlayer.SinglePlayer.PlayerID = ID;
            return PartialView("_PlayerStatPartialPage", partialPlayer);
        }

        [HttpGet]
        public ActionResult ViewGames()
        {
            GameViewModel gamesVMList = new GameViewModel();

            List<LogicGame> boGames = _gameBLL.ReadGames();
            gamesVMList.GamesList = GamesListMap(boGames);

            return View("ViewGames", gamesVMList);
        }

        [HttpGet]
        public ActionResult ViewSingleGame(int GameID)
        {
            GameViewModel gamesVMList = new GameViewModel();

            List<LogicGame> boGames = _gameBLL.ReadGameByGameID(GameID);
            gamesVMList.GamesList = GamesListMap(boGames);

            return View("ViewSingleGame", gamesVMList);
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

        static List<GameModel> GamesListMap(List<LogicGame> boGamesList)
        {
            List<GameModel> gamesVMList = new List<GameModel>();
            foreach (LogicGame boGame in boGamesList)
            {
                GameModel game = new GameModel();

                var type_boGame = boGame.GetType();
                var type_game = game.GetType();

                foreach (var field_boGame in type_boGame.GetFields())
                {
                    var field_game = type_game.GetField(field_boGame.Name);
                    field_game.SetValue(game, field_boGame.GetValue(boGame));
                }

                foreach (var prop_boGame in type_boGame.GetProperties())
                {
                    var prop_game = type_game.GetProperty(prop_boGame.Name);
                    prop_game.SetValue(game, prop_boGame.GetValue(boGame));
                }
                gamesVMList.Add(game);
            }
            return gamesVMList;

        }

        static LogicTeam HomeStatsMap(GameViewModel gameVM)
        {
            //had to manually Map bc gameVM.SingleGame has Away Team values and wanted to separately send down Home & Away Team Stats
            GameModel gameTeamStats = gameVM.SingleGame;

            LogicTeam boTeamStats = new LogicTeam();
            boTeamStats.TeamID = gameTeamStats.HomeTeamID;
            boTeamStats.TeamName = gameTeamStats.HomeTeamName;
            boTeamStats.TeamFouls = gameTeamStats.HomeTeamFouls;
            boTeamStats.TeamTurnovers = gameTeamStats.HomeTeamTurnOvers;
            boTeamStats.TeamShotAttempts = gameTeamStats.HomeTeamShotAttempts;
            boTeamStats.TeamShotMakes = gameTeamStats.HomeTeamShotMakes;
            boTeamStats.TeamScore = gameTeamStats.HomeTeamScore;

            return boTeamStats;
        }

        static LogicTeam AwayStatsMap(GameViewModel gameVM)
        {
            GameModel gameTeamStats = gameVM.SingleGame;

            LogicTeam boAwayStats = new LogicTeam();
            boAwayStats.TeamID = gameTeamStats.AwayTeamID;
            boAwayStats.TeamName = gameTeamStats.AwayTeamName;
            boAwayStats.TeamFouls = gameTeamStats.AwayTeamFouls;
            boAwayStats.TeamTurnovers = gameTeamStats.AwayTeamTurnOvers;
            boAwayStats.TeamShotAttempts = gameTeamStats.AwayTeamShotAttempts;
            boAwayStats.TeamShotMakes = gameTeamStats.AwayTeamShotMakes;
            boAwayStats.TeamScore = gameTeamStats.AwayTeamScore;
            return boAwayStats;

        }

        static List<LogicPlayer> PlayerStatsMap(List<PlayerModel> gamePlayers)
        {
            //had to manually Map bc gameVM.SingleGame has Away Team values and only want to store Home Team values
            // List<PlayerModel> playerVMStats = gameVM.homeTeamPlayers;
            List<LogicPlayer> boPlayerStatList = new List<LogicPlayer>();

            foreach (PlayerModel playerStat in gamePlayers)
            {
                LogicPlayer boPlayerStats = new LogicPlayer();
                boPlayerStats.PlayerID = playerStat.PlayerID;
                boPlayerStats.TeamID = playerStat.TeamID;
                boPlayerStats.PlayerAssists = playerStat.PlayerAssists;
                boPlayerStats.PlayerFouls = playerStat.PlayerFouls;
                boPlayerStats.PlayerPoints = playerStat.PlayerPoints;
                boPlayerStats.QuarterPlayed = playerStat.QuarterPlayed;
                boPlayerStats.PlayerShotAttempts = playerStat.PlayerShotAttempts;
                boPlayerStats.PlayerShotMakes = playerStat.PlayerShotMakes;

                boPlayerStatList.Add(boPlayerStats);
            }
            return boPlayerStatList;
        }
    }
}