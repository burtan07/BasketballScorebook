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
    public class PlayerDataAccess
    {
        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        string _FileLocation = @"C:/Users/Admin2/Desktop/ErrorLog.txt";

        public void CreatePlayer(DataPlayer daPlayer)
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_CreatePlayer", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@Team_ID", daPlayer.TeamID);
                cmd.Parameters.AddWithValue("@PlayerLastName", daPlayer.PlayerLastName);
                cmd.Parameters.AddWithValue("@PlayerFirstInitial", daPlayer.PlayerFirstInitial);
                cmd.Parameters.AddWithValue("@PlayerRole", daPlayer.PlayerRole);
                cmd.Parameters.AddWithValue("@PlayerJerseyNum", daPlayer.JerseyNum);
                cmd.Parameters.AddWithValue("@PlayerAssists", daPlayer.PlayerAssists);
                cmd.Parameters.AddWithValue("@PlayerFouls", daPlayer.PlayerFouls);
                cmd.Parameters.AddWithValue("@PlayerPoints", daPlayer.PlayerPoints);
                cmd.Parameters.AddWithValue("@PlayerQuartersPlayed", daPlayer.QuarterPlayed);
                cmd.Parameters.AddWithValue("@PlayerShotAttempts", daPlayer.PlayerShotAttempts);
                cmd.Parameters.AddWithValue("@PlayerShotMakes", daPlayer.PlayerShotMakes);

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


        public List<DataPlayer> ReadPlayersTable()
        {
            List<DataPlayer> ldaPlayerList = new List<DataPlayer>();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_ReadPlayersTable", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DataPlayer daReadPlayer = new DataPlayer();
                        daReadPlayer.PlayerID = rdr.GetInt32(rdr.GetOrdinal("PlayerID"));
                        daReadPlayer.TeamID = rdr.GetInt32(rdr.GetOrdinal("Team_ID"));
                        daReadPlayer.TeamName = (string)rdr["TeamName"];
                        daReadPlayer.PlayerLastName = (string)rdr["PlayerLastName"];
                        daReadPlayer.PlayerFirstInitial = (string)rdr["PlayerFirstInitial"];
                        daReadPlayer.PlayerRole = (string)rdr["PlayerRole"];
                        daReadPlayer.JerseyNum = rdr.GetInt32(rdr.GetOrdinal("PlayerJerseyNum"));
                        daReadPlayer.PlayerAssists = rdr.GetInt32(rdr.GetOrdinal("PlayerAssists"));
                        daReadPlayer.PlayerFouls = rdr.GetInt32(rdr.GetOrdinal("PlayerFouls"));
                        daReadPlayer.PlayerPoints = rdr.GetInt32(rdr.GetOrdinal("PlayerPoints"));
                        daReadPlayer.QuarterPlayed = rdr.GetInt32(rdr.GetOrdinal("PlayerQuartersPlayed"));
                        daReadPlayer.PlayerShotAttempts = rdr.GetInt32(rdr.GetOrdinal("PlayerShotAttempts"));
                        daReadPlayer.PlayerShotMakes = rdr.GetInt32(rdr.GetOrdinal("PlayerShotMakes"));

                        ldaPlayerList.Add(daReadPlayer);
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
            return ldaPlayerList;
        }


        public void UpdatePlayerByPlayerID(DataPlayer daPlayer)
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_UpdatePlayerByPlayerID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@PlayerID", daPlayer.PlayerID);
                cmd.Parameters.AddWithValue("@Team_ID", daPlayer.TeamID);
                cmd.Parameters.AddWithValue("@PlayerRole", daPlayer.PlayerRole);
                cmd.Parameters.AddWithValue("@PlayerJerseyNum", daPlayer.JerseyNum);
                cmd.Parameters.AddWithValue("@PlayerAssists", daPlayer.JerseyNum);
                cmd.Parameters.AddWithValue("@PlayerFouls", daPlayer.PlayerFouls);
                cmd.Parameters.AddWithValue("@PlayerPoints", daPlayer.PlayerPoints);
                cmd.Parameters.AddWithValue("@PlayerQuartersPlayed", daPlayer.QuarterPlayed);
                cmd.Parameters.AddWithValue("@PlayerShotAttempts", daPlayer.PlayerShotAttempts);
                cmd.Parameters.AddWithValue("@PlayerShotMakes", daPlayer.PlayerShotMakes);

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

        public void DeletePlayerByPlayerID(int playerToDelete)
        {
            //takes in Player to Delete's ID & Sends to SP_DeletePlayerByPlayerID 

            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_DeletePlayerByPlayerID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@PlayerID", playerToDelete);

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


