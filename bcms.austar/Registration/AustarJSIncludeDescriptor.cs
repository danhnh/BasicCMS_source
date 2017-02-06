using bcms.austar.Controllers;
using BetterCms.Core.Modules;
using BetterCms.Core.Modules.Projections;
using bcms.austar.Content.Resources;
namespace bcms.austar.Registration {
	public class AustarJSIncludeDescriptor : JsIncludeDescriptor {
		public AustarJSIncludeDescriptor(CmsModuleDescriptor module) : base(module, "bcms.austar") {
			Links = new IActionProjection[] {
					new JavaScriptModuleLinkTo<UniversityController>(this, "loadSiteSettingsUniversitiesUrl", c => c.ListUniversities()),
					new JavaScriptModuleLinkTo<UniversityController>(this, "loadUniversitiesUrl", c => c.UniversitiesList(null)),
					new JavaScriptModuleLinkTo<UniversityController>(this, "saveUniversityUrl", c => c.SaveUniversity(null)),
					new JavaScriptModuleLinkTo<UniversityController>(this, "deleteUniversityUrl", c => c.DeleteUniversity(null, null)),
				};
			Globalization = new IActionProjection[] {
				new JavaScriptModuleGlobalization(this,"creatorDialogTitle", ()=> UniversityGlobalization.creatorDialog_Title),
				new JavaScriptModuleGlobalization(this,"editorDialogTitle", ()=>UniversityGlobalization.editDialog_Title),
			};
		}
	}
}