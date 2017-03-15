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
    public class UserDataAccess
    {
        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        string _FileLocation = @"C:/Users/Admin2/Desktop/ErrorLog.txt";

        public void CreateUser(DataUser daUser)  //Registers new Users
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_RegisterUser", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@UserName", daUser.UserName);
                cmd.Parameters.AddWithValue("@UserPassword", daUser.Password);
                cmd.Parameters.AddWithValue("@FirstName", daUser.FirstName);
                cmd.Parameters.AddWithValue("@LastName", daUser.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", daUser.EmailAddress);
                cmd.Parameters.AddWithValue("@SecurityQuestion", daUser.SecurityQuestion);
                cmd.Parameters.AddWithValue("@SecurityAnswer", daUser.SecurityAnswer);

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

        public List<DataUser> ReadUsers()  //Reads the current List of Users, calls SP_ReadUsers, returns the List of Users
        {
            List<DataUser> ldaUserList = new List<DataUser>();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_ReadUsers", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DataUser daReadUser = new DataUser();
                        daReadUser.UserID = rdr.GetInt32(rdr.GetOrdinal("UserID"));
                        daReadUser.RoleID = rdr.GetInt32(rdr.GetOrdinal("RoleID"));
                        daReadUser.UserName = (string)rdr["UserName"];
                        daReadUser.Password = (string)rdr["UserPassword"];
                        daReadUser.FirstName = (string)rdr["FirstName"];
                        daReadUser.LastName = (string)rdr["LastName"];
                        daReadUser.EmailAddress = (string)rdr["EmailAddress"];
                        daReadUser.SecurityQuestion = (string)rdr["SecurityQuestion"];
                        daReadUser.SecurityAnswer = (string)rdr["SecurityAnswer"];

                        ldaUserList.Add(daReadUser);
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
            return ldaUserList;
        }


        public void UpdateUser(DataUser updatedDAuser)  //Sends Updated RoleID for a User to SP_UpdatedUserByUserID
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_UpdateUserByUserID", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@UserID", updatedDAuser.UserID);
                cmd.Parameters.AddWithValue("@RoleID", updatedDAuser.RoleID);

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


        public void DeleteUser(int UserIDtoDelete)  //takes in UserID & Sends to SP_DeleteUser
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_DeleteUser", lConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@UserID", UserIDtoDelete);

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

        public void UserPasswordReset(DataUser daUserPasswordReset)  //Takes in ResetPassword and sends into DB
        {
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_UserPasswordResetByUserID", lConnection);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@UserID", daUserPasswordReset.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", daUserPasswordReset.Password);

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

        public DataUser GetUserByUsername(string username)
        //takes in daUser that needs Login check & calls "SP_checkLogin", sends userToCheck to SP, SP returns UserRole if UserName & Password match, this method is called in userBLL
        {
            DataUser checkedUser = new DataUser();
            SqlConnection lConnection = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand("sp_CheckUserLogin", lConnection);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                lConnection.Open();

                cmd.Parameters.AddWithValue("@UserName", username);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //DataUser daUser = new DataUser();
                        checkedUser.UserName = (string)rdr["UserName"];
                        checkedUser.Password = (string)rdr["UserPassword"];
                        checkedUser.RoleID = rdr.GetInt32(rdr.GetOrdinal("RoleID"));

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
            return checkedUser;

        }
    }
}
