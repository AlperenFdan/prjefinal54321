using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Controllers
{
    [UserLoginControl]
    public class SCartController : Controller
    {
        Mylib ML = new Mylib();
        EH_Store db = new EH_Store();
        // GET: SCart
        public ActionResult Index()
        {
            List<object> list = new List<object>();
            int id = (Session["User"] as Users).ID;
            foreach (SCart item in db.SCarts.Where(x => x.UserID == id))
            {
                var Product = ML.GetArticle(item.ProductType, item.ProductID);
                if (Product != null)
                {
                    list.Add(Product);
                }
             
            }
            List<SCart > list2 = new List<SCart>();
            foreach (SCart item in db.SCarts)
            {
                if (ML.GetArticle(item.ProductType,item.ID) != null)
                {
                    list2.Add(item);
                }
                else
                {
                    SCart removeitem = db.SCarts.Find(item.ID);
                    db.SCarts.Remove(removeitem);
                   
                }
            }
            db.SaveChanges();
            ViewBag.ShoppingCarts = list2;
            return View(list);
        }
        public ActionResult ChangeQuantity(int Pid, bool up,string typ)
        {
            if (Session["User"] != null)
            {
                int uid = (Session["User"] as Users).ID;

                SCart cart = db.SCarts.FirstOrDefault(s => s.UserID == uid && s.ProductID == Pid);
                var Product = ML.GetArticle(typ, Pid);
                Type type = Product.GetType();
                int stok = (int)type.GetProperty("stock").GetValue(Product); 
                int id = (int)type.GetProperty("ID").GetValue(Product);
                string name = type.GetProperty("Name").GetValue(Product).ToString();
                if (cart != null && Product != null)
                {
                    if (up)
                    {
                        if (cart.Quantity + 1 > stok)
                        {
                            return RedirectToAction("Index");
                        }
                        cart.Quantity++;
                    }
                    else if (cart.Quantity > 1)
                    {
                        cart.Quantity--;
                    }

                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int Pid, string typ, int q)
        {
            var Product = ML.GetArticle(typ, Pid); 
            Type type = Product.GetType();
            int stok = (int)type.GetProperty("stock").GetValue(Product); 
            bool Ac = (bool)type.GetProperty("IsActived").GetValue(Product);
            if (Product != null|| stok < q && Ac)
            {
               
                int id = (int)type.GetProperty("ID").GetValue(Product);
                string name = type.GetProperty("Name").GetValue(Product).ToString();
                decimal price = (decimal)type.GetProperty("Price").GetValue(Product);
                int idu = (Session["User"] as Users).ID;
                SCart sc = db.SCarts.FirstOrDefault(x => x.UserID == idu && x.ProductID == Pid && x.ProductType == typ);
                if (sc != null)
                {
                    if (sc.Quantity + q < stok)
                    {
                        sc.Quantity += q;
                        db.SaveChanges();
                    }
                }
                else
                {
                    SCart sCart = new SCart
                    {
                        UserID = (Session["User"] as Users).ID,
                        ProductID = Pid,
                        ProductType = typ.ToString(),
                        Quantity = q,
                        price = price,

                    };
                    db.SCarts.Add(sCart);
                    db.SaveChanges();
                }
            
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Bu üründen yeterli stoğumuz yoktur";
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            int idu = (Session["User"] as Users).ID;
            SCart sc = db.SCarts.FirstOrDefault(x => x.ID == id && x.UserID == idu );
            if (sc != null)
            {
                db.SCarts.Remove(sc); 
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}