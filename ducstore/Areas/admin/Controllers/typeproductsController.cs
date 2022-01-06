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

namespace ducstore.Areas.admin.Controllers
{
    public class typeproductsController : Controller
    {
        private Store db = new Store();

        // GET: admin/typeproducts
        public ActionResult Index(int page = 1)
        {
            return View(db.typeproducts.ToList().ToPagedList(page, 10));
        }

     
        // GET: admin/typeproducts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // GET: admin/typeproducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/typeproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "typeproductid,typeproductname")] typeproduct typeproduct)
        {
            if (ModelState.IsValid)
            {
                db.typeproducts.Add(typeproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeproduct);
        }

        // GET: admin/typeproducts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // POST: admin/typeproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "typeproductid,typeproductname")] typeproduct typeproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeproduct);
        }

        // GET: admin/typeproducts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // POST: admin/typeproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            typeproduct typeproduct = db.typeproducts.Find(id);
            db.typeproducts.Remove(typeproduct);
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
