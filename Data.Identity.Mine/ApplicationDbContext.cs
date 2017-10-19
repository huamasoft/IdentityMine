using Data.Identity.Mine.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity.Mine
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

        public List<ClaimInfo> ClaimInfos { get; set; }
    }

    //public class RoleClaimInfo
    //{
    //    public TYPE Type { get; set; }
    //}

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer(new IdentityDbInit());
        //}

        public DbSet<ClaimInfo> ClaimInfos { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Web.MyIdentity.Models.AppRole> IdentityRoles { get; set; }

        //public System.Data.Entity.DbSet<Web.MyIdentity.Models.AppRole> IdentityRoles { get; set; }

        //public System.Data.Entity.DbSet<Web.MyIdentity.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
