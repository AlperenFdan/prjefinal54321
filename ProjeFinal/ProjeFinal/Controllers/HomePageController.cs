using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ProjeFinal.Controllers
{
    [UserLoginControl]
    public class HomePageController : Controller
    {
        // GET: HomePage
        EH_Store db = new EH_Store();
        Mylib ml = new Mylib();
        List<object> list = new List<object>() ;
        public ActionResult Index()
        {
            List<object> list = new List<object>();
            foreach (Product item in db.Products)
            {
                list.Add(ml.GetArticle(item.ProductType,item.ProductID));
            }
            list = new List<object>(list
                .Where(x => x.GetType().GetProperty("Name") != null) 
                .OrderBy(x => x.GetType().GetProperty("Name")?.GetValue(x)) 
                .ToList());
            return View(list);
        }
    }
}