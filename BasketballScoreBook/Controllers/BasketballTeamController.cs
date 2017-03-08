using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballScoreBook.Controllers
{
    public class BasketballTeamController : Controller
    {
        // GET: BasketballTeam
        public ActionResult Index()
        {
            return View();
        }
    }
}