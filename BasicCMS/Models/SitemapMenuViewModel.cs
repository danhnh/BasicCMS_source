using System;
using System.Collections.Generic;

namespace BasicCMS.Models
{
	public class SitemapMenuViewModel
	{
		public string LanguageCode { get; set; }
		public bool RenderIFrame { get; set; }
		public List<MenuItemViewModel> ObsoleteMenuItems { get; set; }
		public List<MenuItemViewModel> MenuItems { get; set; }
		public List<string> LanguageCodes { get; set; }
		public Guid? PageId { get; set; }
		public List<MenuTranslationItem> PageTranslations { get; set; }
		public BetterCms.Module.Api.Operations.Pages.Pages.Page.PageModel Page { get; set; }
	}
}