using BlogMvcApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BlogMvcApp.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() : base("blogDb") //istersen web confige bir string daha ekle kullanıcılar farklı veritabanında tut farketmez
        {
       

        }
    
    }
}