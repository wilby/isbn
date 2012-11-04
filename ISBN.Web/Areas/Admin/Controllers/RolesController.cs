using ISBN.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ISBN.Web.Areas.Admin.Controllers
{
    [HandleError]
    [Authorize(Roles = "Admins")]
    public class RolesController : Controller
    {
        
        public ActionResult Index()
        {
            var roles = Roles.GetAllRoles();

            return View(roles);
        }

        public ActionResult Add()
        {
            return View("Add", new Role());
        }

        [HttpPost]
        public ActionResult Add(Role model)
        {
            
            try
            {                
                Roles.CreateRole(model.RoleName);
                return RedirectToAction("Index");                
            }
            catch (ArgumentException ae)
            {
                this.ModelState.AddModelError("RoleName", ae.Message);
            }            
            catch (ProviderException pe)
            {
                this.ModelState.AddModelError("RoleName", pe.Message);
            }
            return View("Add", model);
        }

        public ActionResult Edit(string id)
        {
            return View("Edit", new RoleEdit() { RoleName = id, OldRoleName = id});
        }

        [HttpPost]
        public ActionResult Edit(RoleEdit model)
        {
            if (Roles.RoleExists(model.OldRoleName))
                Roles.DeleteRole(model.OldRoleName);

            Roles.CreateRole(model.RoleName);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Roles.DeleteRole(id);                   
            return RedirectToAction("Index");
        }

        public ActionResult UsersInRole(string roleName)
        {
            RoleUsers model = new RoleUsers();

            model.RoleName = roleName;
            model.UserNames = Roles.GetUsersInRole(roleName);

            return View(model);
        }

        public ActionResult DeleteUserRole(string username, string roleName)
        {
            Roles.RemoveUserFromRole(username, roleName);
            return RedirectToAction("UsersInRole", new { roleName = roleName });
        }

        public ActionResult AddUserToRole(string roleName)
        {
            var model = new RoleUsers() { RoleName = roleName };
            model.UserNames = Roles.GetUsersInRole(roleName);

            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddUserToRole(RoleUsers model)
        {
            Roles.AddUserToRole(model.UserName, model.RoleName);
            return RedirectToAction("AddUserToRole", new { roleName = model.RoleName });
        }
    }
}
