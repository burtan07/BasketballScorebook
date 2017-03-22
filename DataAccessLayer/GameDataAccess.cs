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

    }
}
