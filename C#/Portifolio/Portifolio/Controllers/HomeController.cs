﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portifolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Projetos()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }

        public ActionResult Skills() 
        {
            return View();
        }
    }
}