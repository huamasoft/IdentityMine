using Data.Identity.Mine;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Identity.Mine.Infrastructure
{
    public class AppUserManager : UserManager<ApplicationUser>
    {

        public AppUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static AppUserManager Create(
                IdentityFactoryOptions<AppUserManager> options,
                IOwinContext context)
        {
            var db = context.Get<ApplicationDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}