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
    public class TeamDataAccess
    {
        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        string _FileLocation = @"C:/Users/Admin2/Desktop/ErrorLog.txt";

        public void CreateTeam(DataTeam daTeam)
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_CreateTeam", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@TeamName", daTeam.TeamName);


                //cmd.Parameters.AddWithValue("@TeamTimeouts", daTeam.TeamTimeouts);
                //cmd.Parameters.AddWithValue("@TeamFouls", daTeam.TeamFouls);
                //cmd.Parameters.AddWithValue("@TeamTurnovers", daTeam.TeamTurnovers);
                //cmd.Parameters.AddWithValue("@TeamShotAttempts", daTeam.TeamShotAttempts);
                //cmd.Parameters.AddWithValue("@TeamShotMakes", daTeam.TeamShotMakes);
                //cmd.Parameters.AddWithValue("@TeamScore", daTeam.TeamScore);

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

        public List<DataTeam> ReadTeams()
        {
            List<DataTeam> ldaTeamList = new List<DataTeam>();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_ReadTeamTable", lConnection);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DataTeam daReadTeam = new DataTeam();
                        daReadTeam.TeamID = rdr.GetInt32(rdr.GetOrdinal("TeamID"));
                        daReadTeam.TeamName = (string)rdr["TeamName"];


                        ldaTeamList.Add(daReadTeam);
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
            return ldaTeamList;
        }


        public void UpdateTeamName(DataTeam daTeam)
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_UpdateTeamByTeamID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();
                cmd.Parameters.AddWithValue("@TeamID", daTeam.TeamID);
                cmd.Parameters.AddWithValue("@TeamName", daTeam.TeamName);

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

        public void DeleteTeamByTeamID(int daTeamID)
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_DeleteTeamByTeamID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@TeamID", daTeamID);

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
