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
    public class PlayerController : Controller
    {
        static PlayerBusinessLogic _playerBLL = new PlayerBusinessLogic();
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InputPlayerInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InputPlayerInfo(PlayerViewModel playerVM)
        {
            LogicPlayer boPlayer = new LogicPlayer();



            return 
        }


    }
}