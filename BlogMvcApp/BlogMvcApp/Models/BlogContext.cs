using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    //database iliskisini kurmak için bu isimde class oluşturup  aşağıya :DbContxt i yazıp nuget packet dan entity framework indirip yukarıya  using System.Data.Entity; şunu ekliyoruz.
    public class BlogContext:DbContext
    {

        //:base("blogVt") bunu eklersen bu isimde oluşturur veri tabanını veya
        //connection string eklersen webconfige gidip blogVt ye uyan connection stringi bulur ve belirttiğin isimde veritabanı oluşturur
        //en sağlıklısı connection stringle yapmak çünkü bu sayede veritabanı ekleyebilir ve çıkartabiliriz
        //yada yayınlayacağımız zaman uzaktaki veritabanına bağlanabiliriz.
        public BlogContext() : base("blogDb")
        {
          

        }
        public DbSet<Blog> Bloglar { get; set; }  
        public DbSet<Category> Kategoriler { get; set; }
        public DbSet<Comment> Yorumlar { get; set; }

        public DbSet<Image> Resimler { get; set; }
    }
}