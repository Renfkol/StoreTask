using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Store.WebUI.Infrastructure.Identity;

namespace Store.WebUI.Infrastructure.Identity
{
    public class AppDbInitializer : DropCreateDatabaseAlways<IdentityDbContext>
    {
        protected override void Seed(IdentityDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            var role1 = new ApplicationRole { Name = "admin", Description="admin" };
            var role2 = new ApplicationRole { Name = "user", Description = "user" };
            
            roleManager.Create(role1);
            roleManager.Create(role2);

            //var admin = new ApplicationUser { Email = "admin@store.com", UserName = "admin@store.com", DefaultAddress="none", FullName="admin"};
            //string password = "123456!!";
            //var result = userManager.Create(admin, password);

            //if (result.Succeeded)
            //{
            //    userManager.AddToRole(admin.Id, role1.Name);
            //    userManager.AddToRole(admin.Id, role2.Name);
            //}

            base.Seed(context);
        }
    }
}