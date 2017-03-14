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


        public List<LogicUser> ViewUsers()
        {
            List<DataUser> dataUsers = new List<DataUser>();
            List<LogicUser> userList = UserMap(dataUsers);
            return userList;
        }

        static List<LogicUser> UserMap(List<DataUser> dataUsers)
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

        public void CreateUser(LogicUser boUser)  //Calls Mapper BOuser to DAuser & sends down to DAL
        {
            DataUser daUser = Map(boUser);
            _userDAL.CreateUser(daUser);
        }

        public void UpdateUser(LogicUser boUpdatedUser)
        {
            DataUser updatedDAuser = Map(boUpdatedUser);
            _userDAL.UpdateUser(updatedDAuser);
        }

        public void DeleteUser(int UserIDtoDelete)
        {
            _userDAL.DeleteUser(UserIDtoDelete);
        }

        public void UserPasswordReset(LogicUser boUserPasswordReset)
        {
            DataUser daUserPasswordReset = Map(boUserPasswordReset);
            _userDAL.UserPasswordReset(daUserPasswordReset);
        }
    }
}
