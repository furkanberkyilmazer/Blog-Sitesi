using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogModel
    {

        //Burayı sadece Home saydasında 
        //her bilginin görüntülenmesini istemediğimiz için bu hale getirdik
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        public string Resim { get; set; }
    

        public DateTime EklenmeTarihi { get; set; }

        public bool Onay { get; set; }

        public bool Anasayfa { get; set; }

        public int CategoryId { get; set; }
    }
}