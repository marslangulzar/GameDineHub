using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
[assembly: OwinStartup(typeof(FireStreetPizza.Startup))]
namespace FireStreetPizza
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                CookieName = "Repoerteq_FireStreetPizza_2020_Auth_Cookies",
                ExpireTimeSpan = TimeSpan.FromDays(30)
            });

            app.MapSignalR();
        }
    }
}