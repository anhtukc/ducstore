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
    public class productsController : Controller
    {
        private Store db = new Store();

        // GET: products
        public ActionResult Index(int page = 1)
        {
            var products = db.products.Include(p => p.provider).Include(p => p.typeproduct);
            return View(products.ToList().ToPagedList(page, 10));
        }

        public JsonResult GetTypeproduct()
        {
            var type = db.typeproducts.Select(s => new { s.typeproductid, s.typeproductname }).ToList();
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProduct()
        {
            var product = db.products.Select(s => new { s.typeproductid, s.productid, s.productname, s.promotion, s.picture, s.price, s.quantity }).ToList();
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        // GET: products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
      

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
