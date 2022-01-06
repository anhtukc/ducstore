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
    public class newsController : Controller
    {
        private Store db = new Store();

        // GET: admin/news
        public ActionResult Index(int page = 1)
        {
            return View(db.news.ToList().ToPagedList(page, 10));
        }

     
        // GET: admin/news/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/news/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/news/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "newsid,newstittle,author,picture,content")] news news, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                var filename = Path.GetFileName(picture.FileName);
                var dicrectiontosave = Server.MapPath(Url.Content("~/Content/anh"));
                var pathtosave = Path.Combine(dicrectiontosave, filename);
                picture.SaveAs(pathtosave);
                news.picture = filename;
                news.daycreate = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                news.newsid = Guid.NewGuid();
                db.news.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: admin/news/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/news/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "newsid,newstittle,author,picture,content")] news news, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                var filename = Path.GetFileName(picture.FileName);
                var dicrectiontosave = Server.MapPath(Url.Content("~/Content/anh"));
                var pathtosave = Path.Combine(dicrectiontosave, filename);
                picture.SaveAs(pathtosave);
                news.picture = filename;
                news.daycreate = DateTime.Now;

            }
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: admin/news/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/news/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            news news = db.news.Find(id);
            db.news.Remove(news);
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
