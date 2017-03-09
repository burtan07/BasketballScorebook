﻿using System;
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

                cmd.Parameters.AddWithValue("@TeamID", daPlayer.PlayerTeamID);
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
    }
}
