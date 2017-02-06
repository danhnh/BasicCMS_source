
using bcms.austar.Commands;
using bcms.austar.ViewModels;
using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using Microsoft.Web.Mvc;
using System.Web.Mvc;

namespace bcms.austar.Controllers {
	[BcmsAuthorize]
	[ActionLinkArea(AustarModuleDescriptor.ModuleAreaName)]
	public class UniversityController : CmsControllerBase {
		[BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
		public ActionResult ListUniversities() {
			var model = GetCommand<GetUniversityListCommand>().ExecuteCommand(new SearchableGridOptions());
			var view = RenderView("Universities", model);
			return ComboWireJson(model != null, view, model, JsonRequestBehavior.AllowGet);
		}
		[BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
		public ActionResult UniversitiesList(SearchableGridOptions request) {
			request.SetDefaultPaging();
			var model = GetCommand<GetUniversityListCommand>().ExecuteCommand(request);
			return WireJson(model != null, model);
		}
		[HttpPost]
		[BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
		public ActionResult SaveUniversity(UniversityViewModel model) {
			var success = false;
			UniversityViewModel response = null;
			if (ModelState.IsValid) {
				response = GetCommand<SaveUniversityCommand>().ExecuteCommand(model);
				if (response != null) {
					if (model.Id.HasDefaultValue()) {
						Messages.AddSuccess("University is created.");
					}

					success = true;
				}
			}

			return WireJson(success, response);
		}
		[HttpPost]
		[BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
		public ActionResult DeleteUniversity(string id, string version) {
			var request = new UniversityViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
			var success = GetCommand<DeleteUniversityCommand>().ExecuteCommand(request);
			if (success) {
				if (!request.Id.HasDefaultValue()) {
					Messages.AddSuccess("University deleted successful");
				}
			}

			return WireJson(success);
		}
	}
}