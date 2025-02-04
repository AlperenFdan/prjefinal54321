using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeFinal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        EH_Store db = new EH_Store();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
            Users u = db.Users.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            if (u != null)
            {
                if (u.IsActive)
                {
                    Session["User"] = u;
                    return RedirectToAction("Index","HomePage");
                }
                else
                {
                    ViewBag.error = "Hesabın askıya alınmış!";
                }
            }
            else
            {
                ViewBag.error = "Lütfen tüm alanları doldurunuz!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "HomePage");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Users model)
        {
            if (Mylib.PropControl(model))
            {
                model.SignUpTime = DateTime.Now;
               
                Users us = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (us != null)
                {
                    ViewBag.error = "Bu mail adresi başka bir hesap için kullanılıyor";
                    return View();
                }
                db.Users.Add(model);
                db.SaveChanges();
                Session["User"] = model;
                return RedirectToAction("Index", "HomePage");
            }
            else
            {
                ViewBag.error = "Lütfen tüm boşlukları doldurunuz";
            }
            return View(model);
        }
    }
}