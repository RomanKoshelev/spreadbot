﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// FilterConfig.cs
// romak_000, 2015-03-19 15:48

using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}