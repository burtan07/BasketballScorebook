﻿using System;
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
        public ActionResult CreatePlayer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlayer(PlayerViewModel playerVM)
        {
            LogicPlayer boPlayer = Map(playerVM);
            _playerBLL.CreatePlayer(boPlayer);
            
            return View();
        }

        static LogicPlayer Map(PlayerViewModel playerVM)  //Maps userVM to boUser
        {
            LogicPlayer boPlayer = new LogicPlayer();

            var type_playerVM = playerVM.SinglePlayer.GetType();
            var type_boPlayer = boPlayer.GetType();

            foreach (var field_playerVM in type_playerVM.GetFields())
            {
                var field_boPlayer = type_boPlayer.GetField(field_playerVM.Name);
                field_boPlayer.SetValue(boPlayer, field_playerVM.GetValue(playerVM.SinglePlayer));
            }

            foreach (var prop_playerVM in type_playerVM.GetProperties())
            {
                var prop_boPlayer = type_boPlayer.GetProperty(prop_playerVM.Name);
                prop_boPlayer.SetValue(boPlayer, prop_playerVM.GetValue(playerVM.SinglePlayer));
            }
            return boPlayer;
        }


    }
}