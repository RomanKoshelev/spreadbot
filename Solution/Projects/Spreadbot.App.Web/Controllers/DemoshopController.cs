﻿using System.Web.Mvc;
using Spreadbot.Core.System;

namespace Spreadbot.App.Web
{
    // !>> Controller | DemoshopController
    public class DemoshopController : Controller
    {
        // ===================================================================================== []
        // Index
        public ActionResult Index()
        {
            DemoshopModel.SaveChanges();
            return View(new DemoshopModel());
        }

        // ===================================================================================== []
        // UpdateItem
        [HttpPost]
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")] DemoshopItem item)
        {
            DemoshopModel.SaveItem(item);
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // Add Task
        public ActionResult AddTask()
        {
            DemoshopModel.PublishItemOnEbay();
            DemoshopModel.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // RunChannelTasks
        public ActionResult RunChannelTasks()
        {
            Dispatcher.RunChannelTasks(DemoshopModel.ChannelTasksTodo);
            DemoshopModel.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // ProceedChannelTasks
        public ActionResult ProceedChannelTasks()
        {
            Dispatcher.ProceedChannelTasks(DemoshopModel.ChannelTasksInprocess);
            DemoshopModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}