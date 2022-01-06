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
    public class shoppingController : Controller
    {
        private Store db = new Store();

        // GET: shopping
        public ActionResult Index()
        {
            var sellbills = db.sellbills.Include(s => s.customer);
            return View(sellbills.ToList());
        }



        public void CheckCustomerInfo(customer ct)
        {
            customer customer_search = db.customers.Where(c => c.phonenumber == ct.phonenumber).FirstOrDefault();
            if (customer_search == null)
            {
                customer_search = ct;
                customer_search.customerid = Guid.NewGuid();
                db.customers.Add(customer_search);
                db.SaveChanges();
            }
        }
        public string Create(string phonenumber, int totalprice, sellbilldetail[] list)
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
                item.product = db.products.Find(item.productid);
                item.sellbillid = sb.sellbillid;
                sb.sellbilldetails.Add(item);
            }
            db.sellbills.Add(sb);

            db.SaveChanges();


            return "CreateSuccessful";

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
