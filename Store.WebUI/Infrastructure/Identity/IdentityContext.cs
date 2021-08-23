using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Store.WebUI.Infrastructure.Identity;
using Microsoft.AspNet.Identity;


namespace Store.WebUI.Infrastructure.Identity
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityContext():base("IdentityDb"){}
        static IdentityContext()
        {
            //Database.SetInitializer<IdentityContext>(new AppDbInitializer()); //не с Identity
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }

}