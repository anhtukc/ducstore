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
using System.IO;
namespace ducstore.Areas.admin.Controllers
{
    public class productsController : Controller
    {
        private Store db = new Store();

        // GET: admin/products
        public ActionResult Index(int page = 1)
        {
            var products = db.products.Include(p => p.provider).Include(p => p.typeproduct);
            return View(products.ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.products
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.productname.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 5));
        }

        // GET: admin/products/Details/5
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

        // GET: admin/products/Create
        public ActionResult Create()
        {
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername");
            ViewBag.typeproductid = new SelectList(db.typeproducts, "typeproductid", "typeproductname");
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,typeproductid,providerid,productname,promotion,picture,price,quantity,description")] product product, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                var filename = Path.GetFileName( picture.FileName);
                var dicrectiontosave = Server.MapPath(Url.Content("~/Content/anh"));
                var pathtosave = Path.Combine(dicrectiontosave, filename);
                picture.SaveAs(pathtosave);
                product.picture = filename;
                
            }
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", product.providerid);
            ViewBag.typeproductid = new SelectList(db.typeproducts, "typeproductid", "typeproductname", product.typeproductid);
            return View(product);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", product.providerid);
            ViewBag.typeproductid = new SelectList(db.typeproducts, "typeproductid", "typeproductname", product.typeproductid);
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,typeproductid,providerid,productname,promotion,picture,price,quantity,description")] product product, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                var filename = Path.GetFileName(picture.FileName);
                var dicrectiontosave = Server.MapPath(Url.Content("~/Content/anh/"));
                var pathtosave = Path.Combine(dicrectiontosave, filename);
                picture.SaveAs(pathtosave);
                product.picture = filename;
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", product.providerid);
            ViewBag.typeproductid = new SelectList(db.typeproducts, "typeproductid", "typeproductname", product.typeproductid);
            return View(product);
        }

        // GET: admin/products/Delete/5
        public ActionResult Delete(string id)
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

        // POST: admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
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
