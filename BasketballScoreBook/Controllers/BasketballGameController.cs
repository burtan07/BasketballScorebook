using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballScoreBook.Controllers
{
    public class BasketballGameController : Controller
    {
        // GET: BasketballGame
        public ActionResult Index()
        {
            return View();
        }
    }
}