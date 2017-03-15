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
    public class UserBusinessLogic
    {
        static UserDataAccess _userDAL = new UserDataAccess();

        static DataUser Map(LogicUser boUser)   //Maps BOuser to DAuser
        {
            DataUser daUser = new DataUser();

            var type_BOuser = boUser.GetType();
            var type_DAuser = daUser.GetType();

            foreach (var field_BOuser in type_BOuser.GetFields())
            {
                var field_DAuser = type_DAuser.GetField(field_BOuser.Name);
                field_DAuser.SetValue(daUser, field_BOuser.GetValue(boUser));
            }

            foreach (var property_BOuser in type_BOuser.GetProperties())
            {
                var property_DAuser = type_DAuser.GetProperty(property_BOuser.Name);
                property_DAuser.SetValue(daUser, property_BOuser.GetValue(boUser));
            }

            return daUser;
        }

        static LogicUser Map(DataUser daUser) //Maps daUser to boUser
        {
            LogicUser boUser = new LogicUser();

            var type_daUser = daUser.GetType();
            var type_boUser = boUser.GetType();

            foreach (var field_daUser in type_daUser.GetFields())
            {
                var field_boUser = type_boUser.GetField(field_daUser.Name);
                field_boUser.SetValue(boUser, field_daUser.GetValue(daUser));
            }

            foreach (var prop_daUser in type_daUser.GetProperties())
            {
                var prop_boUser = type_boUser.GetProperty(prop_daUser.Name);
                prop_boUser.SetValue(boUser, prop_daUser.GetValue(daUser));
            }

            return boUser;

        }


        public List<LogicUser> ViewUsers()  //Calls ListMapper method and returns Logic List 
        {
            List<DataUser> dataUsers = new List<DataUser>();
            List<LogicUser> userList = UserMap(dataUsers);
            return userList;
        }

        static List<LogicUser> UserMap(List<DataUser> dataUsers) //List Mapper from daUserList to LogicUserList
        {
            List<LogicUser> logicUsers = new List<LogicUser>();
            foreach (DataUser dUser in dataUsers)
            {
                LogicUser lUser = new LogicUser();
                lUser.UserID = dUser.UserID;
                lUser.RoleID = dUser.RoleID;
                lUser.UserName = dUser.UserName;
                lUser.Password = dUser.Password;
                logicUsers.Add(lUser);
            }
            return logicUsers;
        }

        public void CreateUser(LogicUser boUser)  //Creates User Calls Mapper BOuser to DAuser & sends down to DAL
        {
            DataUser daUser = Map(boUser);
            _userDAL.CreateUser(daUser);
        }

        public void UpdateUser(LogicUser boUpdatedUser) //Maps LogicUser to daUser & Sends Updated daUser to DAL
        {
            DataUser updatedDAuser = Map(boUpdatedUser);
            _userDAL.UpdateUser(updatedDAuser);
        }

        public void DeleteUser(int UserIDtoDelete) //Takes in int for UserID (from Presentation) that will be deleted & sends to DAL
        {
            _userDAL.DeleteUser(UserIDtoDelete);
        }

        public void UserPasswordReset(LogicUser boUserPasswordReset) //Takes in boUser's reset password, Maps to daUser & Sends to DAL
        {
            DataUser daUserPasswordReset = Map(boUserPasswordReset);
            _userDAL.UserPasswordReset(daUserPasswordReset);
        }

        public bool CheckLogin(string LoginPassword, string StoredPassword)
        {
            //takes in UserLoginToCheck from Presentation, Maps to daUser, Sends daUserLoginToCheck to DAL, 
            //returnedCheckeduser catches return from CheckLogin in DAL, returnedCheckeduser is Mapped to LogicUser and sent back up to Presentation

            bool passwordCorrect = false;
            
            if (LoginPassword == StoredPassword)
            {
                passwordCorrect = true;
            }
            else
            {
                passwordCorrect = false;
            }
            return passwordCorrect;
        }

        public LogicUser GetUserByUsername(string Username)
        {
            DataUser daReturnUser = _userDAL.GetUserByUsername(Username);
            LogicUser returnedUser = Map(daReturnUser);
            

            return returnedUser;
        }
    }
}
