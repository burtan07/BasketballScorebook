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


        static List<LogicPlayer> ListMap(List<DataPlayer> daPlayers)  //Maps daPlayerList to boPlayerList
        {
            List<LogicPlayer> logicPlayers = new List<LogicPlayer>();
            foreach(DataPlayer daPlayer in daPlayers)
            {
                LogicPlayer boPlayer = new LogicPlayer();

                var type_daPlayer = daPlayer.GetType();
                var type_boPlayer = boPlayer.GetType();

                foreach(var field_daPlayer in type_daPlayer.GetFields())
                {
                    var field_boPlayer = type_boPlayer.GetField(field_daPlayer.Name);
                    field_boPlayer.SetValue(boPlayer, field_daPlayer.GetValue(daPlayer));
                }
                foreach(var prop_daPlayer in type_daPlayer.GetProperties())
                {
                    var prop_boPlayer = type_boPlayer.GetProperty(prop_daPlayer.Name);
                    prop_boPlayer.SetValue(boPlayer, prop_daPlayer.GetValue(daPlayer));
                }

                logicPlayers.Add(boPlayer);

            }
            
            return logicPlayers;
        }


        static DataPlayer Map(LogicPlayer boPlayer)
        {
            DataPlayer daPlayer = new DataPlayer();

            var type_boPlayer = boPlayer.GetType();
            var type_daPlayer = daPlayer.GetType();

            foreach(var field_boPlayer in type_boPlayer.GetFields())
            {
                var field_daPlayer = type_daPlayer.GetField(field_boPlayer.Name);
                field_daPlayer.SetValue(daPlayer, field_boPlayer.GetValue(boPlayer));
            }

            foreach(var prop_boPlayer in type_boPlayer.GetProperties())
            {
                var prop_daPlayer = type_daPlayer.GetProperty(prop_boPlayer.Name);
                prop_daPlayer.SetValue(daPlayer, prop_boPlayer.GetValue(boPlayer));
            }
            return daPlayer;
        }

    }
}
