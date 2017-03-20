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
   public  class GameBusinessLogic
    {

        static GameDataAccess _gameDAL = new GameDataAccess();

        public void CreateGame(LogicGame boGame)
        {
            DataGame daGame = Map(boGame);
            _gameDAL.CreateGame(daGame);
        }



        static DataGame Map(LogicGame boGame) //Maps LogicGame to DataGame
        {
            DataGame daGame = new DataGame();

            var type_boGame = boGame.GetType();
            var type_daGame = daGame.GetType();

            foreach(var field_boGame in type_boGame.GetFields())
            {
                var field_daGame = type_daGame.GetField(field_boGame.Name);
                field_daGame.SetValue(daGame, field_boGame.GetValue(boGame));

            }

            foreach (var prop_boGame in type_boGame.GetProperties())
            {
                var prop_daGame = type_daGame.GetProperty(prop_boGame.Name);
                prop_daGame.SetValue(daGame, prop_boGame.GetValue(boGame));
            }

            return daGame;


        }
    }
}
