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
    public class GraphicsCardForMangerController : Controller
    {
        string MyPictureFolder = "~/MyDataForFinalProject/Photos/";
        EH_Store db = new EH_Store();
        // GET: ManagerPanel/GraphicsCard
        public ActionResult Index()
        {
            return View(db.GraphicsCards);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
            }
            GraphicsCard gc = db.GraphicsCards.Find(id);
            if (gc == null)
            {

            }
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true ), "ID", "Name", gc.CategoryID);
            ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true ), "ID", "Name", gc.BrandID);

            return View(gc);
        }
        [HttpPost]
        public ActionResult Edit(int id,GraphicsCard gr, HttpPostedFileBase hpf)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (Mylib.PropControl(gr))
                    {
                        db.Entry(gr).State = System.Data.Entity.EntityState.Modified;
                        if (hpf != null)
                        {

                            FileInfo fi = new FileInfo(hpf.FileName);

                            if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                            {
                                if (gr.imgs != null)
                                {
                                    FileInfo Oldfi = new FileInfo(gr.imgs);
                                    Oldfi.Delete();
                                }

                                Guid filename = Guid.NewGuid();
                                string fullname = filename + fi.Extension;
                                hpf.SaveAs(Server.MapPath(MyPictureFolder + fullname));
                                gr.imgs = fullname;
                                db.SaveChanges();

                            }
                            else
                            {
                                ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true), "ID", "Name");
                                ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true), "ID", "Name");
                                ViewBag.error = "Resim olarak Sadece JPEG,JPG ve PNG kabul ediyoruz";
                                return View();
                            }
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true), "ID", "Name");
                        ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true), "ID", "Name");
                        ViewBag.error = "Lütfen her yeri kontrol ediniz";
                        return View(db.Memorys.Find(gr.ID));
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
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true), "ID", "Name");
            ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true), "ID", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult Create(GraphicsCard model, HttpPostedFileBase hpf)
        {
            try
            {
                if (Mylib.PropControl(model) && hpf != null)
                {
                    model.CreationTime = DateTime.Now;
                    model.IsDeleted = false;
                    FileInfo fi = new FileInfo(hpf.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                    {
                        Guid filename = Guid.NewGuid();
                        string fullname = filename + fi.Extension;
                        hpf.SaveAs(Server.MapPath(MyPictureFolder + fullname));
                        model.imgs = fullname;
                        db.GraphicsCards.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true), "ID", "Name");
                        ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true), "ID", "Name");
                        ViewBag.error = "Resim olarak Sadece JPEG,JPG ve PNG kabul ediyoruz";
                        return View();
                    }
                }
                else
                {
                    ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.IsActive == true), "ID", "Name");
                    ViewBag.BrandID = new SelectList(db.Brands.Where(c => c.IsActive == true), "ID", "Name");
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
            GraphicsCard model = db.GraphicsCards.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost, ActionName("DeletePage")]
        public ActionResult DeleteConfirmed(int id)
        {
            GraphicsCard md = db.GraphicsCards.Find(id);
            if (md != null)
            {
                db.GraphicsCards.Remove(md); db.SaveChanges();
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