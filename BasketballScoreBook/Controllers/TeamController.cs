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
    public class TeamController : Controller
    {
        static TeamBusinessLogic _teamBLL = new TeamBusinessLogic();
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeam(TeamViewModel teamVM)
        {
            LogicTeam boTeam = Map(teamVM);
            _teamBLL.CreateTeam(boTeam);
            return View(); //redirect to action()
        }

        

        static LogicTeam Map(TeamViewModel teamVM)
        {
            LogicTeam boTeam = new LogicTeam();

            var type_teamVM = teamVM.SingleTeam.GetType();
            var type_boTeam = boTeam.GetType();

            foreach(var field_teamVM in type_teamVM.GetFields())
            {
                var field_boTeam = type_boTeam.GetField(field_teamVM.Name);
                field_boTeam.SetValue(boTeam, field_teamVM.GetValue(teamVM.SingleTeam));
            }

            foreach (var prop_teamVM in type_teamVM.GetProperties())
            {
                var prop_boTeam = type_boTeam.GetProperty(prop_teamVM.Name);
                prop_boTeam.SetValue(boTeam, prop_teamVM.GetValue(teamVM.SingleTeam));
            }

            return boTeam;
        }

    }
}