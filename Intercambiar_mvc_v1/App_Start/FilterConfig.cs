﻿using System.Web;
using System.Web.Mvc;

namespace Intercambiar_mvc_v1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}