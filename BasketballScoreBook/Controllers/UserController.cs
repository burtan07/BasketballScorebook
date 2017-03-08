using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasketballScoreBook.Models;

namespace BasketballScoreBook.Controllers
{
    public class UserController : Controller
    {
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
            return View("Login");

        }

        [HttpGet]
        public ActionResult Game()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game()
        {
            return View(); 
        }

    }
}