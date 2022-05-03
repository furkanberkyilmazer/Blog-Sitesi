using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogInitializer:DropCreateDatabaseIfModelChanges<BlogContext>
    {

        protected override void Seed(BlogContext context)
        {
            List<Category> kategoriler = new List<Category>()
            {
                new Category()  {KategoriAdi="C#" },
                new Category()  {KategoriAdi="Asp.net MVC" },
                new Category()  {KategoriAdi="Asp.net WebForm" },
                new Category()  {KategoriAdi="Windows Form" },
            };
            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();

            List<Blog> bloglar = new List<Blog>()
            {
                new Blog(){Baslik="C#  Hakkında",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-10),Anasayfa=true,Onay=true,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik="MVC proje ",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-10),Anasayfa=true,Onay=true,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik="C# ASP.Net Hakkında",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-15),Anasayfa=false,Onay=false,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik="C# ASP.NetCore Hakkında",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-10),Anasayfa=true,Onay=true,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="1.jpg",CategoryId=2},
                new Blog(){Baslik="C# Delegates Hakkında",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-30),Anasayfa=false,Onay=true,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="2.jpg",CategoryId=2},
                new Blog(){Baslik="C# Generic Hakkında C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Aciklama="C# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-20),Anasayfa=true,Onay=true,Icerik="C# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates HakkındaC# Delegates Hakkında",Resim="2.jpg",CategoryId=2},
             };

            foreach (var item in bloglar)
            {
                context.Bloglar.Add(item);
            }
            context.SaveChanges();

            List<Comment> yorumlar = new List<Comment>()
            {
                new Comment()  {Yorum="Çok iyi",BlogId=1 ,KullaniciAdi="furkanberk"},
                new Comment()  {Yorum="harika",BlogId=2,KullaniciAdi="brkylmzr" },
           
            };
            foreach (var item in yorumlar)
            {
                context.Yorumlar.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}