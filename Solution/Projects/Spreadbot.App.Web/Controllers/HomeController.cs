﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HomeController.cs
// romak_000, 2015-03-19 14:07

using System.Web.Mvc;

namespace Spreadbot.App.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}