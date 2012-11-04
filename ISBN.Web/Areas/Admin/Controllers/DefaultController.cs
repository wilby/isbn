using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISBN.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Admins")]
    public class DefaultController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

    }
}
