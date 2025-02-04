﻿using System.Web.Mvc;

namespace ProjeFinal.Areas.ManagerPanel
{
    public class ManagerPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ManagerPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ManagerPanel_default",
                "ManagerPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                  new[] { "ProjeFinal.Areas.ManagerPanel.Controllers" }
            );
        }
    }
}