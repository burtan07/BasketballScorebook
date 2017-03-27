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
    public class GameBusinessLogic
    {

        static GameDataAccess _gameDAL = new GameDataAccess();

        public void CreateGame(LogicGame boGame)
        {
            DataGame daGame = Map(boGame);
            _gameDAL.CreateGame(daGame);
        }

        public List<LogicGame> ReadGames()
        {
            List<DataGame> daGames = _gameDAL.ReadGamesTable();
            List<LogicGame> boGames = ListMap(daGames);

            return boGames;
        }

        public List<LogicGame> ReadGameByGameID(int GameID)
        {

            List<DataGame> daGames = _gameDAL.ReadGamesByGameID(GameID);
            List<LogicGame> boGames = ListMap(daGames);

            return boGames;
        }



        static DataGame Map(LogicGame boGame) //Maps LogicGame to DataGame
        {
            DataGame daGame = new DataGame();

            var type_boGame = boGame.GetType();
            var type_daGame = daGame.GetType();

            foreach (var field_boGame in type_boGame.GetFields())
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

        static List<LogicGame> ListMap(List<DataGame> daGames)
        {
            List<LogicGame> boGames = new List<LogicGame>();
            foreach (DataGame dGame in daGames)
            {
                LogicGame boGame = new LogicGame();

                var type_daGame = dGame.GetType();
                var type_boGame = boGame.GetType();

                foreach(var field_daGame in type_daGame.GetFields())
                {
                    var field_boGame = type_boGame.GetField(field_daGame.Name);
                    field_boGame.SetValue(boGame, field_daGame.GetValue(dGame));
                }

                foreach(var prop_daGame in type_daGame.GetProperties())
                {
                    var prop_boGame = type_boGame.GetProperty(prop_daGame.Name);
                    prop_boGame.SetValue(boGame, prop_daGame.GetValue(dGame));
                }

                boGames.Add(boGame);
            }

            return boGames;
        }
    }
}
