using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Controllers
{
    [UserLoginControl]
    public class FavroitesController : Controller
    {
        // GET: Favroites
        EH_Store db = new EH_Store();
        Mylib ml = new Mylib();
        public ActionResult Index()
        {
            List<object> list = new List<object>();
            int iud = (Session["User"] as Users).ID;
            foreach (Favorites item in db.Favorites.Where(x => iud == x.userID))
            {
                if (ml.GetArticle(item.ProductType, item.ProductID) != null)
                {
                    list.Add(ml.GetArticle(item.ProductType, item.ProductID));
                }
              
            }
            return View(list);
        }
        public ActionResult AddToFav(int Pid, string typ)
        {
            var Product = ml.GetArticle(typ, Pid);
            //Type type = Product.GetType();
            //object ConvertedArticle = Convert.ChangeType(Product, type);
            int iud = (Session["User"] as Users).ID;
            if (db.Favorites.FirstOrDefault(x=> x.ProductType == typ && x.ProductID == Pid && x.userID == iud) == null)
            {
                Favorites item = new Favorites();
                item.ProductType = typ;
                item.ProductID = Pid;
                item.userID = (Session["User"] as Users).ID;
                db.Favorites.Add(item);
                db.SaveChanges();
            }

            return RedirectToAction("Detail", typ, new { id = Pid });
        }
        public ActionResult Delete(int Pid, string typ)
        {
            var Product = ml.GetArticle(typ, Pid);
            int iud = (Session["User"] as Users).ID;
            Favorites item = db.Favorites.FirstOrDefault(x => x.ProductType == typ && x.ProductID == Pid && x.userID == iud );
            Type type = Product.GetType();
            object ConvertedArticle = Convert.ChangeType(Product, type);
            if (item != null)
            {
               
                db.Favorites.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Detail", typ, new { id = Pid });
        }
    }
}