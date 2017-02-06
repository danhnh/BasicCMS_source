using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Principal;
using BetterCms.Core;
using BetterCms.Core.Environment.Host;
using System.Reflection;

namespace BasicCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
		//private static ICmsHost cmsHost;
		protected void Application_Start()
        {
			//cmsHost = CmsContext.RegisterHost();
			AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			//cmsHost.OnApplicationStart(this);
		}

		//protected void Application_BeginRequest()
		//{
		//	// [YOUR CODE]

		//	cmsHost.OnBeginRequest(this);
		//}

		//protected void Application_EndRequest()
		//{
		//	// [YOUR CODE]

		//	cmsHost.OnEndRequest(this);
		//}

		//protected void Application_Error()
		//{
		//	// [YOUR CODE]

		//	cmsHost.OnApplicationError(this);
		//}

		protected void Application_End() {
			// [YOUR CODE]
			HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
																					BindingFlags.NonPublic
																					| BindingFlags.Static
																					| BindingFlags.GetField,
																					null,
																					null,
																					null);

			if (runtime == null)
				return;

			string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
																			 BindingFlags.NonPublic
																			 | BindingFlags.Instance
																			 | BindingFlags.GetField,
																			 null,
																			 runtime,
																			 null);

			string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
																		   BindingFlags.NonPublic
																		   | BindingFlags.Instance
																		   | BindingFlags.GetField,
																		   null,
																		   runtime,
																		   null);
			Common.Logging.LogManager.GetLogger("TEST").Debug(String.Format("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
										 shutDownMessage,
										 shutDownStack));
			//cmsHost.OnApplicationEnd(this);
		}

		//protected void Application_AuthenticateRequest(object sender, EventArgs e)
		//{
		//	// [YOUR CODE]

		//	// Uncomment following source code for a quick Better CMS test if you don't have implemented users authentication. 
		//	// Do not use this code for production!

		//	//var roles = new[] { "BcmsEditContent", "BcmsPublishContent", "BcmsDeleteContent", "BcmsAdministration" };
		//	//var principal = new GenericPrincipal(new GenericIdentity("TestUser"), roles);
		//	//HttpContext.Current.User = principal;


		//	cmsHost.OnAuthenticateRequest(this);
		//}

	}
}
