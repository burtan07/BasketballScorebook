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
                cmd.Parameters.AddWithValue("@TeamLevel", daTeam.TeamLevel);
                cmd.Parameters.AddWithValue("@TeamGender", daTeam.TeamGender);
                cmd.Parameters.AddWithValue("@TeamHomeStatus", daTeam.TeamHomeStatus);
                //cmd.Parameters.AddWithValue("@TeamTimeouts", daTeam.TeamTimeouts);
                //cmd.Parameters.AddWithValue("@TeamFouls", daTeam.TeamFouls);
                //cmd.Parameters.AddWithValue("@TeamTurnovers", daTeam.TeamTurnovers);
                //cmd.Parameters.AddWithValue("@TeamShotAttempts", daTeam.TeamShotAttempts);
                //cmd.Parameters.AddWithValue("@TeamShotMakes", daTeam.TeamShotMakes);
                //cmd.Parameters.AddWithValue("@TeamScore", daTeam.TeamScore);

                cmd.ExecuteNonQuery();
            }

            catch(SqlException error)
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
