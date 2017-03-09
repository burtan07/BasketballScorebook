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
    public class UserDataAccess
    {
        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        string _FileLocation = @"C:/Users/Admin2/Desktop/ErrorLog.txt";

        public void CreateUser(DataUser daUser)
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

        public List<DataUser> ReadUsers()
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
            return ldaUserList;
        }
    }
}