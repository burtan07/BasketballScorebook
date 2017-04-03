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

    public class PlayerBusinessLogic
    {
        static PlayerDataAccess _playerDAL = new PlayerDataAccess();
        static TeamBusinessLogic _teamBLL = new TeamBusinessLogic();

        public void CreatePlayer(LogicPlayer boPlayer)
        {
            DataPlayer daPlayer = Map(boPlayer);
            _playerDAL.CreatePlayer(daPlayer);
        }

        public List<LogicPlayer> ViewPlayers()
        {
            List<DataPlayer> daPlayers = _playerDAL.ReadPlayersTable();
            List<LogicPlayer> boPlayerList = ListMap(daPlayers);

            return boPlayerList;
        }

        public void UpdatePlayerByPlayerID(LogicPlayer boUpdatedPlayer)
        {
            DataPlayer daUpdatedPlayer = Map(boUpdatedPlayer);
            _playerDAL.UpdatePlayerByPlayerID(daUpdatedPlayer);

        }

        public void DeletePlayerByPlayerID(int playerToDelete)  //Deletes Player by PlayerID (player selected), sends to DAL
        {
            _playerDAL.DeletePlayerByPlayerID(playerToDelete);
        }

        public void DeletePlayerByTeamID(int TeamID)
        {
            //Deletes Players on Certain Team, Calls ViewTeamPlayers Method, sends in TeamID, Loops thru that list & sends playerID to DeletePLayer

            List<LogicPlayer> playersOnTeam = ViewTeamPlayers(TeamID);
            foreach (LogicPlayer teamPlayer in playersOnTeam)
            {
                DeletePlayerByPlayerID(teamPlayer.PlayerID);
            }
            _teamBLL.DeleteTeamByTeamID(TeamID);

        }


        public void ReadPlayersByPlayerID(List<LogicPlayer> gamePlayers)
        {

            foreach (LogicPlayer boPlayer in gamePlayers)
            {
                DataPlayer daStoredPlayer = _playerDAL.ReadPlayerByPlayerID(boPlayer.PlayerID);
                LogicPlayer boStoredPlayer = Map(daStoredPlayer);

                LogicPlayer boUpdatedPlayer = boStoredPlayer;
               // boUpdatedPlayer.TeamID = boPlayer.TeamID;
               // boUpdatedPlayer.PlayerRole = boPlayer.PlayerRole;
                //boUpdatedPlayer.JerseyNum = boPlayer.JerseyNum;
                boUpdatedPlayer.PlayerAssists += boPlayer.PlayerAssists;
                boUpdatedPlayer.PlayerFouls += boPlayer.PlayerFouls;
                boUpdatedPlayer.PlayerPoints += boPlayer.PlayerPoints;
                boUpdatedPlayer.QuarterPlayed += boPlayer.QuarterPlayed;
                boUpdatedPlayer.PlayerShotAttempts += boPlayer.PlayerShotAttempts;
                boUpdatedPlayer.PlayerShotMakes += boPlayer.PlayerShotMakes;

                DataPlayer daUpdatedPlayer = Map(boUpdatedPlayer);

                _playerDAL.UpdatePlayerByPlayerID(daUpdatedPlayer);
            }
        }

        //public DataPlayer AddPlayerStatsByPlayerID(LogicPlayer boStoredStats)
        //{

        //}


        public List<LogicPlayer> ViewTeamPlayers(int TeamID)
        {
            List<DataPlayer> daPlayers = _playerDAL.ReadPlayersByTeamID(TeamID);
            List<LogicPlayer> boTeamPlayersList = ListMap(daPlayers);

            return boTeamPlayersList;
        }


        static List<LogicPlayer> ListMap(List<DataPlayer> daPlayers)  //Maps daPlayerList to boPlayerList
        {
            List<LogicPlayer> logicPlayers = new List<LogicPlayer>();
            foreach (DataPlayer daPlayer in daPlayers)
            {
                LogicPlayer boPlayer = new LogicPlayer();

                var type_daPlayer = daPlayer.GetType();
                var type_boPlayer = boPlayer.GetType();

                foreach (var field_daPlayer in type_daPlayer.GetFields())
                {
                    var field_boPlayer = type_boPlayer.GetField(field_daPlayer.Name);
                    field_boPlayer.SetValue(boPlayer, field_daPlayer.GetValue(daPlayer));
                }
                foreach (var prop_daPlayer in type_daPlayer.GetProperties())
                {
                    var prop_boPlayer = type_boPlayer.GetProperty(prop_daPlayer.Name);
                    prop_boPlayer.SetValue(boPlayer, prop_daPlayer.GetValue(daPlayer));
                }

                logicPlayers.Add(boPlayer);

            }

            return logicPlayers;
        }


        static DataPlayer Map(LogicPlayer boPlayer)  //Maps boPlayer to daPlayer
        {
            DataPlayer daPlayer = new DataPlayer();

            var type_boPlayer = boPlayer.GetType();
            var type_daPlayer = daPlayer.GetType();

            foreach (var field_boPlayer in type_boPlayer.GetFields())
            {
                var field_daPlayer = type_daPlayer.GetField(field_boPlayer.Name);
                field_daPlayer.SetValue(daPlayer, field_boPlayer.GetValue(boPlayer));
            }

            foreach (var prop_boPlayer in type_boPlayer.GetProperties())
            {
                var prop_daPlayer = type_daPlayer.GetProperty(prop_boPlayer.Name);
                prop_daPlayer.SetValue(daPlayer, prop_boPlayer.GetValue(boPlayer));
            }
            return daPlayer;
        }

        static LogicPlayer Map(DataPlayer daPlayer)
        {
            LogicPlayer boPlayer = new LogicPlayer();

            var type_daPlayer = daPlayer.GetType();
            var type_boPlayer = boPlayer.GetType();

            foreach (var field_daPlayer in type_daPlayer.GetFields())
            {
                var field_boPlayer = type_boPlayer.GetField(field_daPlayer.Name);
                field_boPlayer.SetValue(boPlayer, field_daPlayer.GetValue(daPlayer));
            }

            foreach (var prop_daPlayer in type_daPlayer.GetProperties())
            {
                var prop_boPlayer = type_boPlayer.GetProperty(prop_daPlayer.Name);
                prop_boPlayer.SetValue(boPlayer, prop_daPlayer.GetValue(daPlayer));
            }

            return boPlayer;
        }

    }
}
