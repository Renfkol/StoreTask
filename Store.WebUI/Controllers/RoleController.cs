using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.WebUI.Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Store.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public RoleController()
        {
            
        }

        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        [Authorize]
        public ActionResult GetMyRole()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}