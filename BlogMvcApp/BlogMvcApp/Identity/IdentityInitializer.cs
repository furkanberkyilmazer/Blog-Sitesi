using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Identity
{
    public class IdentityInitializer: CreateDatabaseIfNotExists<IdentityDataContext> //yoksa oluştur
    {
        protected override void Seed(IdentityDataContext context)
        {
            //roller
            if (!context.Roles.Any(i=>i.Name=="admin"))
            {
                var store=new RoleStore<ApplicationRole>(context);
                var manager=new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" }; ;
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }

            //user
            if (!context.Users.Any(i => i.Name == "furkanberk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name="berk",Surname="yılmazer",UserName="furkanberk",Email="furkan_berk_yilmazer@hotmail.com"};
                manager.Create(user,"1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.Name == "brkylmzr"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "furkan", Surname = "yılmazer", UserName = "brkylmzr", Email = "furkanberkgs1905@hotmail.com" };
                manager.Create(user, "1234567");
               
                manager.AddToRole(user.Id, "user");
            }

            
            base.Seed(context);
        }
    }
}