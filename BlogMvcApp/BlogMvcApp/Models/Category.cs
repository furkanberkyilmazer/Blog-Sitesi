using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class Category
    {
        [Key]  //Eğer Id ye aşağıdaki gibi isim verirsek üstüne bunu yazmamız lazım ve yukardaki using System.ComponentModel.DataAnnotations; koymamız gerek yoksa veritabanı birincil anahtar olduğunu anlamaz
        public int CategoryId { get; set; }
        public string KategoriAdi { get; set; }

        public List<Blog> Bloglar { get; set; } 
    }
}