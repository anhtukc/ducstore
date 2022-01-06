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
    public class receiptbillsController : Controller
    {
        private Store db = new Store();

        // GET: admin/receiptbills
        public ActionResult Index(int page = 1)
        {
            var receiptbills = db.receiptbills.Include(r => r.employee).Include(r => r.provider);
            return View(receiptbills.ToList().ToPagedList(page, 10));
        }

     
        // GET: admin/receiptbills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptbill receiptbill = db.receiptbills.Find(id);
            if (receiptbill == null)
            {
                return HttpNotFound();
            }
            return View(receiptbill);
        }

        // GET: admin/receiptbills/Create
        public ActionResult Create()
        {
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "employeename");
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername");
            return View();
        }

        // POST: admin/receiptbills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "receiptid,employeeid,daycreate,providerid,totalpaid")] receiptbill receiptbill)
        {
            if (ModelState.IsValid)
            {
                receiptbill.receiptid = Guid.NewGuid();
                db.receiptbills.Add(receiptbill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "employeename", receiptbill.employeeid);
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", receiptbill.providerid);
            return View(receiptbill);
        }

        // GET: admin/receiptbills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptbill receiptbill = db.receiptbills.Find(id);
            if (receiptbill == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "employeename", receiptbill.employeeid);
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", receiptbill.providerid);
            return View(receiptbill);
        }

        // POST: admin/receiptbills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "receiptid,employeeid,daycreate,providerid,totalpaid")] receiptbill receiptbill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptbill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "employeename", receiptbill.employeeid);
            ViewBag.providerid = new SelectList(db.providers, "providerid", "providername", receiptbill.providerid);
            return View(receiptbill);
        }

        // GET: admin/receiptbills/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptbill receiptbill = db.receiptbills.Find(id);
            if (receiptbill == null)
            {
                return HttpNotFound();
            }
            return View(receiptbill);
        }

        // POST: admin/receiptbills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            receiptbill receiptbill = db.receiptbills.Find(id);
            db.receiptbills.Remove(receiptbill);
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
