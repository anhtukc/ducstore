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
    public class sellbillsController : Controller
    {
        private Store db = new Store();

        // GET: admin/sellbills
        public ActionResult Index(int page = 1)
        {
            var sellbills = db.sellbills.Include(s => s.customer);
            return View(sellbills.ToList().ToPagedList(page, 10));
        }

     
        // GET: admin/sellbills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            return View(sellbill);
        }

        // GET: admin/sellbills/Create
        public ActionResult Create()
        {
            ViewBag.customerid = new SelectList(db.customers, "customerid", "phonenumber");
            return View();
        }

        // POST: admin/sellbills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        public JsonResult GetAllProduct()
        {
            var list = db.products.Select(pd => new { pd.productid, pd.productname, pd.quantity, pd.price }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void CheckCustomerInfo(customer _ct)
        {
            customer ct = db.customers.Where(c => c.phonenumber == _ct.phonenumber).FirstOrDefault();
            if (ct == null)
            {
                _ct.customerid = Guid.NewGuid();
                db.customers.Add(_ct);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public string Create(string phonenumber,  int totalprice, sellbilldetail[] list)
        {
            customer ct = db.customers.Where(c => c.phonenumber == phonenumber).FirstOrDefault();
            sellbill sb = new sellbill();
            sb.daycreate = DateTime.Now;
            sb.sellbillid = Guid.NewGuid();
            sb.customer = ct;
            sb.customerid = ct.customerid;
            sb.totalpaid = totalprice;
            foreach (var item in list)
            {

                var product = db.products.Find(item.productid);
                product.quantity -= item.quantity;
                item.product = product;
                item.sellbillid = sb.sellbillid;
                sb.sellbilldetails.Add(item);
            }
            db.sellbills.Add(sb);

            db.SaveChanges();


            return "CreateSuccessful";

        }
        // GET: admin/sellbills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerid = new SelectList(db.customers, "customerid", "phonenumber", sellbill.customerid);
            return View(sellbill);
        }

        // POST: admin/sellbills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sellbillid,daycreate,customerid,totalpaid")] sellbill sellbill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellbill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerid = new SelectList(db.customers, "customerid", "phonenumber", sellbill.customerid);
            return View(sellbill);
        }

        // GET: admin/sellbills/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            return View(sellbill);
        }

        // POST: admin/sellbills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            sellbill sellbill = db.sellbills.Find(id);
            db.sellbills.Remove(sellbill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string EditBills(string sellbillid, int totalpaid, sellbilldetail[] list)
        {
            var Guid_sellbill_id = Guid.Parse(sellbillid);
            sellbill sb = db.sellbills.Find(Guid_sellbill_id);
            sb.totalpaid = totalpaid;
            var details = db.sellbilldetails.Where(dt => dt.sellbillid == Guid_sellbill_id);
            foreach (var item in details)
            {
                var product = db.products.Find(item.productid);
                product.quantity += item.quantity;
            }
            db.sellbilldetails.RemoveRange(db.sellbilldetails.Where(detail => detail.sellbillid == Guid_sellbill_id));
            foreach (var item in list)
            {
                var product = db.products.Find(item.productid);
                product.quantity -= item.quantity;
                item.product = product;
                item.sellbillid = sb.sellbillid;               
                sb.sellbilldetails.Add(item);
            }
            db.SaveChanges();
            return "Sửa thành công";
        }



        // POST: Admin/sellbills/Delete/5
        [HttpPost]
        public string Delete(string id)
        {
            var bill_id = Guid.Parse(id);
            sellbill sellbill = db.sellbills.Find(bill_id);
            var details = db.sellbilldetails.Where(dt => dt.sellbillid == bill_id);
            foreach (var item in details)
            {
                var product = db.products.Find(item.productid);
                product.quantity += item.quantity;
            }
            db.sellbilldetails.RemoveRange(db.sellbilldetails.Where(detail => detail.sellbillid == bill_id));
            db.sellbills.Remove(sellbill);
            db.SaveChanges();
            return "DeletedSuccessful";
        }

        [HttpGet]
        public JsonResult GetDetails(string sellbillid)
        {

            var SellBill_Guid = Guid.Parse(sellbillid);

            var billDetails = db.sellbilldetails.Select(b => new
            {
                b.sellbillid,
                b.productid,
                b.productname,
                b.price,
                b.quantity,
                b.grandpaid
            }).Where(b => b.sellbillid == SellBill_Guid).ToList();

            return Json(billDetails, JsonRequestBehavior.AllowGet);


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
