using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Yorum { get; set; }

        public string KullaniciAdi { get; set; }
       

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
       

    }
}