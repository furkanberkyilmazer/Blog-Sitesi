using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult List(int? id, string AnahtarKelime)
        {
           
            var bloglar = db.Bloglar
                .Where(i => i.Onay == true && i.Anasayfa == true)
                             .Select(i => new BlogModel()
                             {
                                 Id = i.Id,
                                 Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                                 Aciklama = i.Aciklama,
                                 EklenmeTarihi = i.EklenmeTarihi,
                                 Anasayfa = i.Anasayfa,
                                 Onay = i.Onay,
                                 Resim = i.Resim,
                                 CategoryId=i.CategoryId
                             }).AsQueryable(); //where ekleyebilmek için dah asonra
            if (string.IsNullOrEmpty(AnahtarKelime)==false)//boş olup olmama kontrolü
            {
                //contains arama
                bloglar = bloglar.Where(i => i.Baslik.Contains(AnahtarKelime) || i.Aciklama.Contains(AnahtarKelime));
            }
            if (id != null)
            {
                bloglar = bloglar.Where(i => i.CategoryId == id);
            }
                            
            return View(bloglar.ToList());

        }

        [Authorize(Roles = "admin")]
        // GET: Blog
        public ActionResult Index()
        {
            var bloglar = db.Bloglar.Include(b => b.Category).OrderByDescending(i=>i.EklenmeTarihi); 
            //orderbydescending i sonradan ekledik eklenme tarihine göreazalan şekilde sıralıyor ki şeni eklenen en üstte gözüküyor.

            return View(bloglar.ToList());
        }


        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi");
            return View();
        }

        [Authorize(Roles = "admin")]
        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Baslik,Aciklama,Resim,Icerik,CategoryId")] Blog blog, [Bind(Include = "Resim,BlogId")] Image image)
        {
            if (ModelState.IsValid)
            {
                blog.EklenmeTarihi = DateTime.Now;
                blog.Onay = false;  //ikisi eklemesekte varsayılan false olarak ekler
                blog.Anasayfa = false;
                if (Request.Files.Count>0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Image/" + filename + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    blog.Resim = "/Image/" + filename + uzanti;
                    image.Resim= "/Image/" + filename + uzanti;
                    image.BlogId = blog.Id;
                }
                db.Bloglar.Add(blog);
                db.Resimler.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        // GET: Blog/Edit/5
        public ActionResult Edit(int? id) //int? null deger alabilmiş olmasını sağlıyor yani blog/düzenle/ ile geldiğinde bir id lamadan geldiğinde
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Resim,Icerik,Onay,Anasayfa,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(blog).State = EntityState.Modified; ekleme tarihi güncellenmeyeceği için sadece istediğimiz alanlar için bir sorgu yapıcaz

                var entity = db.Bloglar.Find(blog.Id);
                if (entity != null)
                {
                    entity.Baslik = blog.Baslik;
                    entity.Aciklama = blog.Aciklama;
                    entity.Resim = blog.Resim;
                    entity.Icerik = blog.Icerik;
                    entity.Onay = blog.Onay;
                    entity.Anasayfa = blog.Anasayfa;
                    entity.CategoryId = blog.CategoryId;
                    db.SaveChanges();
                    TempData["Blog"] = entity; //wiev e bilgi taşıma
                    return RedirectToAction("Index");
                }

               
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Blog/Details/5
        public PartialViewResult YorumPartial(int? id)
        {
            if (id==null)
            {
                return PartialView(db.Yorumlar.ToList());
            }
            else
            {
                  var yorumlar = db.Yorumlar
                    .Where(i => i.BlogId == id); //where ekleyebilmek için dah asonra

               return PartialView(yorumlar.ToList());  
            }
               

               
           
          
        }
        public PartialViewResult YorumYaz()
        {
          
            return PartialView(Session["kullanici"]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumYaz(int id,string yorumum, [Bind(Include = "Id,Yorum,BlogId,KullaniciAdi")] Comment comment)
        {
            if (ModelState.IsValid)
            { 
                comment.Yorum = yorumum;
                comment.BlogId = id;
                comment.KullaniciAdi = Session["kullaniciAdi"].ToString();
                db.Yorumlar.Add(comment);
                db.SaveChanges();
               
                return Redirect(@"/Blog/Details/"+id);
            }

           
            return View(comment);

         
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
