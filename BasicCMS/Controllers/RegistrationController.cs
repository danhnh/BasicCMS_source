using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicCMS.Controllers
{
	[Authorize]
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Create(bcms.austar.ViewModels.HostRegistrationFirstStep vm)
        {
            return View("Step2");
        }
    }
}