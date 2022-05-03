using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class CommentsController : Controller
    {
        private BlogContext db = new BlogContext();




        [Authorize(Roles = "admin")]
        // GET: Comments
        public ActionResult Index()
        {
            var yorumlar = db.Yorumlar.Include(c => c.Blog);
            return View(yorumlar.ToList());
        }

        [Authorize(Roles = "admin")]
        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Yorumlar.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [Authorize(Roles = "admin")]
        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "Baslik");
            return View();
        }

        [Authorize(Roles = "admin")]
        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Yorum,BlogId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Yorumlar.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "Baslik", comment.BlogId);
            return View(comment);
        }

        [Authorize(Roles = "admin")]
        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Yorumlar.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "Baslik", comment.BlogId);
            return View(comment);
        }

        [Authorize(Roles = "admin")]
        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Yorum,BlogId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "Baslik", comment.BlogId);
            return View(comment);
        }

        [Authorize(Roles = "admin")]
        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Yorumlar.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [Authorize(Roles = "admin")]
        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Yorumlar.Find(id);
            db.Yorumlar.Remove(comment);
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
