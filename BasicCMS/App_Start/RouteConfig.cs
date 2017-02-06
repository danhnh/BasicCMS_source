using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BasicCMS
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("SiteController_TestApi", "Site/{action}", new { controller = "Site", action = "SitemapMenu" });
			routes.MapRoute("UniversityController_Api", "University/{action}", new { controller = "University", action = "UniversitiesList" }, namespaces: new string[1]{ "bcms.austar.Controllers" });
			routes.MapRoute("RegistrationController", "Registration/{action}", new { controller = "Registration", action = "Create" }, namespaces: new string[1] { "BasicCMS.Controllers" });

			//routes.MapRoute(
			//	name: "Default",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);
		}
	}
}
