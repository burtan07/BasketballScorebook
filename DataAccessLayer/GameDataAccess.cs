using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class GameDataAccess
    {

        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        string _FileLocation = @"C:/Users/Admin2/Desktop/ErrorLog.txt";

        public void CreateGame(DataGame daGame)
        {

            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_CreateGame", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@HomeTeamID", daGame.HomeTeamID);
                cmd.Parameters.AddWithValue("@HomeTeamName", daGame.HomeTeamName);
                cmd.Parameters.AddWithValue("@HomeTeamScore", daGame.HomeTeamScore);
                cmd.Parameters.AddWithValue("@HomeTeamFouls", daGame.HomeTeamFouls);
                cmd.Parameters.AddWithValue("@HomeTeamTimeOuts", daGame.HomeTeamTimeOuts);
                cmd.Parameters.AddWithValue("@HomeTeamTurnOvers", daGame.HomeTeamTurnOvers);
                cmd.Parameters.AddWithValue("@HomeTeamShotAttempts", daGame.HomeTeamShotAttempts);
                cmd.Parameters.AddWithValue("@HomeTeamShotMakes", daGame.HomeTeamShotMakes);

                cmd.Parameters.AddWithValue("@AwayTeamID", daGame.AwayTeamID);
                cmd.Parameters.AddWithValue("@AwayTeamName", daGame.AwayTeamName);
                cmd.Parameters.AddWithValue("@AwayTeamScore", daGame.AwayTeamScore);
                cmd.Parameters.AddWithValue("@AwayTeamFouls", daGame.AwayTeamFouls);
                cmd.Parameters.AddWithValue("@AwayTeamTimeOuts", daGame.AwayTeamTimeOuts);
                cmd.Parameters.AddWithValue("@AwayTeamTurnOvers", daGame.AwayTeamTurnOvers);
                cmd.Parameters.AddWithValue("@AwayTeamShotAttempts", daGame.AwayTeamShotAttempts);
                cmd.Parameters.AddWithValue("@AwayTeamShotMakes", daGame.AwayTeamShotMakes);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {
                using (StreamWriter lWriter = new StreamWriter(_FileLocation, true))
                {
                    lWriter.WriteLine(error.Message);
                }
            }
            finally
            {
                lConnection.Close();
            }
        }

        public List<DataGame> ReadGamesTable()
        {
            List<DataGame> lGamesList = new List<DataGame>();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_ReadGamesTable", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DataGame daGames = new DataGame();
                        daGames.GameID = rdr.GetInt32(rdr.GetOrdinal("GameID"));
                        daGames.HomeTeamName = (string)rdr["HomeTeamName"];
                        daGames.HomeTeamScore = rdr.GetInt32(rdr.GetOrdinal("HomeTeamScore"));
                       
                        daGames.AwayTeamName = (string)rdr["AwayTeamName"];
                        daGames.AwayTeamScore = rdr.GetInt32(rdr.GetOrdinal("AwayTeamScore"));
                        
                        lGamesList.Add(daGames);
                    }
                }
            }
            catch (SqlException error)
            {
                using (StreamWriter lWriter = new StreamWriter(_FileLocation, true))
                {
                    lWriter.WriteLine(error.Message);
                }
            }
            finally
            {
                lConnection.Close();
            }
            return lGamesList;
        }


        public List<DataGame> ReadGamesByGameID(int GameID)
        {
            List<DataGame> lGamesList = new List<DataGame>();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_ReadGameByGameID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@GameID", GameID);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DataGame daGames = new DataGame();
                      
                        daGames.HomeTeamName = (string)rdr["HomeTeamName"];
                        daGames.HomeTeamScore = rdr.GetInt32(rdr.GetOrdinal("HomeTeamScore"));
                        daGames.HomeTeamFouls = rdr.GetInt32(rdr.GetOrdinal("HomeTeamFouls"));
                        daGames.HomeTeamTimeOuts = rdr.GetInt32(rdr.GetOrdinal("HomeTeamTimeOuts"));
                        daGames.HomeTeamTurnOvers = rdr.GetInt32(rdr.GetOrdinal("HomeTeamTurnOvers"));
                        daGames.HomeTeamShotAttempts = rdr.GetInt32(rdr.GetOrdinal("HomeTeamShotAttempts"));
                        daGames.HomeTeamShotMakes = rdr.GetInt32(rdr.GetOrdinal("HomeTeamShotMakes"));

                        daGames.AwayTeamName = (string)rdr["AwayTeamName"];
                        daGames.AwayTeamScore = rdr.GetInt32(rdr.GetOrdinal("AwayTeamScore"));
                        daGames.AwayTeamFouls = rdr.GetInt32(rdr.GetOrdinal("AwayTeamFouls"));
                        daGames.AwayTeamTimeOuts = rdr.GetInt32(rdr.GetOrdinal("AwayTeamTimeOuts"));
                        daGames.AwayTeamTurnOvers = rdr.GetInt32(rdr.GetOrdinal("AwayTeamTurnOvers"));
                        daGames.AwayTeamShotAttempts = rdr.GetInt32(rdr.GetOrdinal("AwayTeamShotAttempts"));
                        daGames.AwayTeamShotMakes = rdr.GetInt32(rdr.GetOrdinal("AwayTeamShotMakes"));

                        lGamesList.Add(daGames);
                    }
                }
            }
            catch (SqlException error)
            {
                using (StreamWriter lWriter = new StreamWriter(_FileLocation, true))
                {
                    lWriter.WriteLine(error.Message);
                }
            }
            finally
            {
                lConnection.Close();
            }
            return lGamesList;
        }
    }

}

