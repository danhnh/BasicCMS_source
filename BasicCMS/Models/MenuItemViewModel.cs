using BetterCms.Module.Api.Operations.Pages.Sitemaps.Sitemap.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCMS.Models
{
	public class MenuItemViewModel
	{
		public string Caption { get; set; }
		public string Url { get; set; }
		public bool IsPublished { get; set; }
		public Guid? PageId { get; set; }
		public IList<MenuTranslationItem> Translations { get; set; }
		public override string ToString()
		{
			return string.Format("Caption: {0}, Url: {1}", Caption, Url);
		}
	}
}