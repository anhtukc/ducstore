using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ducstore.Models;

namespace ducstore.Controllers
{
    public class homeController : Controller
    {
        private Store db = new Store();

        // GET: home
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.provider).Include(p => p.typeproduct);
            return View(products.ToList());
        }

        public JsonResult GetNew()
        {
            var news = db.news.Select(p => new { p.newsid, p.picture }).ToList();
            return Json(news, JsonRequestBehavior.AllowGet);
        }
        public ActionResult signin()
        {
            
            return View();
        }
        public string signinadmin(string username, string password)
        {
            var acc = db.accounts.Where(p => p.username == username && p.password == password).FirstOrDefault();
            if(acc != null)
            {
                return "ok";
            }
            else
            {
                return username+" "+password;
            }
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
