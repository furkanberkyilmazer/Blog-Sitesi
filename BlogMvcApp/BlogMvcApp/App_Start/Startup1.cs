using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(BlogMvcApp.Startup1))]

namespace BlogMvcApp
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });


       /*     < add key = "owin:AutomaticAppStartup" value = "false" /> web cobfige appsatings in arasına
     
         < add key = "owin:appStartup" value = "BlogMvcApp.Startup1" />
       */

        }
    }
}
