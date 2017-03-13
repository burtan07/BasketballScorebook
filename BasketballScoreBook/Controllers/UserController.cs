using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasketballScoreBook.Models;
using BusinessLogicLayer;
using BusinessLogicLayer.BusinessObjects;

namespace BasketballScoreBook.Controllers
{
    public class UserController : Controller
    {
        static UserBusinessLogic _userBLL = new UserBusinessLogic();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel userVM)
        {
           
            return View();
        }





        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserViewModel userVM)
        {
            LogicUser boUser = Map(userVM);
            _userBLL.CreateUser(boUser);
            return View("Login");

        }




        [HttpGet]
        public ActionResult UpdateUser(int UserID, string UserName, int RoleID) //can this pull the entire user object?
        {
            //needs to display selected Users: FirstName,LastName ,username,password and roleID
            UserViewModel userToUpdate = new UserViewModel();
            userToUpdate.SingleUser.UserID = UserID;
            userToUpdate.SingleUser.UserName = UserName;
            userToUpdate.SingleUser.RoleID = RoleID;

            return View(userToUpdate);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel updatedUserVM)
        {
            //needs to pull updated info for the user
            LogicUser boUpdatedUser = Map(updatedUserVM);
            _userBLL.CreateUser(boUpdatedUser);

            return View("ViewUsers");
        }





        [HttpGet]
        public ActionResult DeleteUser(int UserID)
        {
            //needs to display selected Users: FirstName,LastName ,username,password and roleID 
            UserViewModel userToDelete = new UserViewModel();
            userToDelete.SingleUser.UserID = UserID;
            
            LogicUser boDeleteUser = Map(userToDelete);
            _userBLL.CreateUser(boDeleteUser);


            return View(userToDelete);
        }

       



       [HttpGet]
       public ActionResult PasswordReset(int UserID, string UserName, int RoleID)
        {
            UserViewModel userToUpdate = new UserViewModel();
            userToUpdate.SingleUser.UserID = UserID;
            userToUpdate.SingleUser.UserName = UserName;
            userToUpdate.SingleUser.RoleID = RoleID;

            return View(userToUpdate);
        }

        [HttpPost]
        public ActionResult PasswordReset(int UserID, string ResetPassword)
        {
            UserViewModel updatedUserPassword = new UserViewModel();
            updatedUserPassword.SingleUser.UserID = UserID;
            updatedUserPassword.SingleUser.Password = ResetPassword;

            LogicUser boUpdatedUser = Map(updatedUserPassword);
            _userBLL.CreateUser(boUpdatedUser);

            return View("ViewUsers");
        }





        //[HttpGet]
        //public ActionResult Game()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Game()
        //{
        //    return View(); 
        //}

            [HttpGet]
            public ActionResult ViewUsers()
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.Users = new List<UserModel>();
            List<LogicUser> logicUsers = _userBLL.ViewUsers();
            viewModel.Users = MapUser(logicUsers);
            return View(viewModel);
        }




        static List<UserModel> MapUser(List<LogicUser> LogicUsers)
        {
            List<UserModel> userList = new List<UserModel>();
            foreach (LogicUser lUser in LogicUsers)
            {
                UserModel user = new UserModel();
                user.UserID = lUser.UserID;
                user.UserName = lUser.UserName;
                user.RoleID = lUser.RoleID;
                user.Password = lUser.Password;
                userList.Add(user);
            }
            return userList;
        }
        




        static LogicUser Map(UserViewModel userVM)  //Maps userVM to boUser
        {
            LogicUser boUser = new LogicUser();

            var type_userVM = userVM.SingleUser.GetType();
            var type_boUser = boUser.GetType();

            foreach (var field_userVM in type_userVM.GetFields())
            {
                var field_boUser = type_boUser.GetField(field_userVM.Name);
                field_boUser.SetValue(boUser, field_userVM.GetValue(userVM.SingleUser));
            }

            foreach(var prop_userVM in type_userVM.GetProperties())
            {
                var prop_boUser = type_boUser.GetProperty(prop_userVM.Name);
                prop_boUser.SetValue(boUser, prop_userVM.GetValue(userVM.SingleUser));
            }



            return boUser;
        }

    }
}