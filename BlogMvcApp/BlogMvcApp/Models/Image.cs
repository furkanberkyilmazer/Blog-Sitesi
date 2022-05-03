using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Resim { get; set; }




        public int BlogId { get; set; }
        public Blog Blog { get; set; }



    }
}