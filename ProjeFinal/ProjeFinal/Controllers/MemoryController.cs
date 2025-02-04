using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Controllers
{
    [UserLoginControl]
    public class MemoryController : Controller
    {
        // GET: Memory
        EH_Store db = new EH_Store();
        public ActionResult Index()
        {
            return View(db.Memorys);
        }
        public ActionResult Detail(int id)
        {
            Memory gr = db.Memorys.Find(id);
            string yp = gr.GetType().Name.Split('_')[0];
            int pid = (Session["User"] as Users).ID;
            Favorites item = db.Favorites.FirstOrDefault(x => x.ProductType == yp && x.ProductID == gr.ID && x.userID == pid);
            ViewBag.Fav = item;
            return View(gr);
        }
    }
}