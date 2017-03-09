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
