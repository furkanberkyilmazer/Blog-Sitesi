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
   
    public class CategoryController : Controller
    {
        private BlogContext db = new BlogContext();


        // bunu biz yaptık partialview layout kullanmaz zaten bir viewin parçasıdır 
        public PartialViewResult KategoriListesi() 
        {

            return PartialView(db.Kategoriler.ToList());

        }

        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // GET: Category
        public ActionResult Index()
        {

            // return View(db.Kategoriler.ToList());   Kendi oluşturuğu hali

            var kategoriler = db.Kategoriler
                               .Select(i =>
                                new CategoryModel()
                                {
                                    Id=i.CategoryId,
                                    KategoriAdi = i.KategoriAdi,
                                    BlogSayisi = i.Bloglar.Count()

                                }
                            );

            return View(kategoriler.ToList());
        }

        [Authorize(Roles ="admin")] //giriş yapıp yapmama kontrolü
        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Kategoriler.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }



        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriAdi")] Category category)  //public ActionResult Create([Bind(Include = "CategoryId,KategoriAdi")] Category category) böyleydi categori ıd yi sildik oyle kalsa da sorun olmuyor ama gerek yok zaten otomatik sayi
        {
            //eğer is valid olmasaydı bir alltaki returnü göndermez dışardaki return view i gönderirdi
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }





        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Kategoriler.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }



        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,KategoriAdi")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Kategori"] = category; //wiev e bilgi taşıma
                return RedirectToAction("Index");
            }
            return View(category);
        }



        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Kategoriler.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [Authorize(Roles = "admin")] //giriş yapıp yapmama kontrolü
        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(category);
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
