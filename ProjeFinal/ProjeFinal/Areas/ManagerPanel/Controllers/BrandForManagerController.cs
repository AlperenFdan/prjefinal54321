using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Areas.ManagerPanel.Controllers
{
    [MangerLoginControl]
    public class BrandForManagerController : Controller
    {

        EH_Store db = new EH_Store();

        public ActionResult Index()
        {
            return View(db.Brands);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
            }
            Brand gc = db.Brands.Find(id);
            if (gc == null)
            {

            }
           

            return View(gc);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand gr)
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
        public ActionResult Create(Brand model)
        {
            try
            {
                if (Mylib.PropControl(model) )
                {
                    model.CreationTime = DateTime.Now;
                 
                    db.Brands.Add(model);
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
            Brand model = db.Brands.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost, ActionName("DeletePage")]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand md = db.Brands.Find(id);
            try
            {
                if (md != null)
                {
                    db.Brands.Remove(md);
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