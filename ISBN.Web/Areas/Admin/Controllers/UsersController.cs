using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ISBN.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class UsersController : Controller
    {
        
        public ActionResult Index()
        {
            var users = Membership.GetAllUsers();
            return View("Index", users);
        }

        public ActionResult ResetPassword(string username)
        {
            ViewBag.UserName = username;
            return View("ResetPassword");
        }

        [HttpPost]
        public ActionResult ResetPassword()
        {
            var username = Request.Form["UserName"];
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("", "The user does not exist");
                return View("ResetPassword");
            }
            //TODO: Configure SMTP to pull settings from web.config
            var user = Membership.GetUser(username);
            var newPass = user.ResetPassword();
            MailMessage mm = new MailMessage("webmaster@wcjj.net",user.Email, "Password Reset", 
               string.Format( @"A password reset was requested on your behalf. If you did not request a password reset please contact support.
            
                New Password: {0}", newPass));
            SmtpClient smtp = new SmtpClient("localhost");
            smtp.Send(mm);

            ViewBag.UserName = username;
            return View("ResetPasswordEmailSent");
        }

    }
}
