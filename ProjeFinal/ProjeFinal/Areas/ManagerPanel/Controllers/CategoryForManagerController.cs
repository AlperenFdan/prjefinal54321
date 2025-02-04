using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Areas.ManagerPanel.Controllers
{
    [MangerLoginControl]
    public class CategoryForManagerController : Controller
    {
        // GET: ManagerPanel/Category
        EH_Store db = new EH_Store();

        public ActionResult Index()
        {
            return View(db.Categories);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
            }
            Category gc = db.Categories.Find(id);
            if (gc == null)
            {

            }


            return View(gc);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category gr)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (Mylib.PropControl(gr))
                    {
                        db.Entry(gr).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {

                        ViewBag.error = "Lütfen her yeri kontrol ediniz";
                        return View(db.Brands.Find(gr.ID));
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                    throw;
                }
            }
            return RedirectToAction("Index");

        }
        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            try
            {
                if (Mylib.PropControl(model))
                {
                    model.CreationTime = DateTime.Now;
                  
                    db.Categories.Add(model);
                    db.SaveChanges();



                }
                else
                {

                    ViewBag.error = "Lütfen her yeri kontrol ediniz";
                    return View();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeletePage(int id)
        {
            Category model = db.Categories.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost, ActionName("DeletePage")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category md = db.Categories.Find(id);
            try
            {
                if (md != null)
                {
                    db.Categories.Remove(md);
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                ViewBag.error = "Marka şunada bir ürün tarafından kullanılıyor!";
                return View(md);
                throw;
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