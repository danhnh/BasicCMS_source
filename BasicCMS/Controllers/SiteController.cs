using BasicCMS.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BetterCms.Module.Api;
using BetterCms.Module.Api.Operations.Pages.Sitemaps;
using BetterCms.Module.Api.Operations.Pages.Sitemaps.Sitemap.Tree;
using BetterCms.Module.Api.Operations.Root.Languages;
using BetterCms.Module.Api.Operations.Root.Languages.Language;
using BetterCms.Module.Users.Provider;

using httpContext = System.Web.HttpContext;

namespace BasicCMS.Controllers
{
	public class SiteController : Controller
	{
		private static Guid defaultSitemapId = new Guid("17ABFEE9-5AE6-470C-92E1-C2905036574B");
		private static Guid contentMenuId = new Guid("1ca19fa7-e4a6-41bb-b0aa-a699017606c7");
		private static string defaultLanguageCode = "en-AU";
		public ActionResult SitemapMenu(string languageCode, Guid? pageId)
		{
			var renderIFrame = string.IsNullOrWhiteSpace(languageCode);
			var model = new SitemapMenuViewModel
			{
				ObsoleteMenuItems = new List<MenuItemViewModel>(),
				MenuItems = new List<MenuItemViewModel>(),
				RenderIFrame = renderIFrame,
				PageId = pageId,
				LanguageCode = languageCode
			};

			using (var api = ApiFactory.Create())
			{
				model.LanguageCodes = api.Root.Languages.Get(new GetLanguagesRequest()).Data.Items.Select(l => l.Code).ToList();

				if (string.IsNullOrWhiteSpace(model.LanguageCode)
					&& model.LanguageCodes != null && model.LanguageCodes.Count > 0)
					model.LanguageCode = model.LanguageCodes[0];

				var languageId = GetLanguageId(api, languageCode);
				var sitemapId = GetSitemapId(api, defaultSitemapId);
				if (sitemapId.HasValue)
				{
					//var request = new BetterCms.Module.Api.Operations.Pages.Sitemap.Tree.GetSitemapTreeRequest { SitemapId = sitemapId.Value };
					//request.Data.LanguageId = languageId ?? new Guid();
					//var response = api.Pages.Sitemap.Tree.Get(request);
					//if (response.Data.Count > 0)
					//{
					//	model.ObsoleteMenuItems = response.Data.Select(mi => new MenuItemViewModel { Caption = mi.Title, Url = mi.Url, IsPublished = mi.PageIsPublished, Translations = mi.Translations }).ToList();
					//}

					var request1 = new BetterCms.Module.Api.Operations.Pages.Sitemaps.Sitemap.Tree.GetSitemapTreeRequest { SitemapId = sitemapId.Value };
					request1.Data.LanguageId = languageId ?? new Guid();
					var response1 = api.Pages.SitemapNew.Tree.Get(request1);
					if (response1.Data.Count > 0)
					{
						model.MenuItems = response1.Data.Select(mi => new MenuItemViewModel { PageId = mi.PageId, Caption = mi.Title, Url = mi.Url, IsPublished = mi.PageIsPublished }).ToList();
					}
				}
				var response2 = api.Pages.Page.Translations.Get(new BetterCms.Module.Api.Operations.Pages.Pages.Page.Translations.GetPageTranslationsRequest() { PageId = model.PageId });
				if (response2.Data.TotalCount > 0) {
					model.PageTranslations = response2.Data.Items.Select(x => new MenuTranslationItem() { IsPublished = x.IsPublished, LanguageCode = x.LanguageCode ?? defaultLanguageCode, Url = x.PageUrl }).ToList();
				}
				var response3 = api.Pages.Page.Get(new BetterCms.Module.Api.Operations.Pages.Pages.Page.GetPageRequest() { PageId = model.PageId });
				if (response3.Data != null)
					model.Page = response3.Data;
			}

			return View("Index", model);
		}
		public ActionResult ContentMenu(string languageCode, Guid? pageId, Guid menuId) {
			var renderIFrame = string.IsNullOrWhiteSpace(languageCode);
			var model = new SitemapMenuViewModel {
				ObsoleteMenuItems = new List<MenuItemViewModel>(),
				MenuItems = new List<MenuItemViewModel>(),
				RenderIFrame = renderIFrame,
				PageId = pageId,
				LanguageCode = languageCode
			};

			using (var api = ApiFactory.Create()) {
				model.LanguageCodes = api.Root.Languages.Get(new GetLanguagesRequest()).Data.Items.Select(l => l.Code).ToList();

				if (string.IsNullOrWhiteSpace(model.LanguageCode)
					&& model.LanguageCodes != null && model.LanguageCodes.Count > 0)
					model.LanguageCode = model.LanguageCodes[0];

				var languageId = GetLanguageId(api, languageCode);
				var sitemapId = GetSitemapId(api, menuId);
				if (sitemapId.HasValue) {
					var request1 = new BetterCms.Module.Api.Operations.Pages.Sitemaps.Sitemap.Tree.GetSitemapTreeRequest { SitemapId = sitemapId.Value };
					request1.Data.LanguageId = languageId ?? new Guid();
					var response1 = api.Pages.SitemapNew.Tree.Get(request1);
					if (response1.Data.Count > 0) {
						model.MenuItems = response1.Data.Select(mi =>
							new MenuItemViewModel { PageId = mi.PageId, Caption = mi.Title,
							Url = mi.Url, IsPublished = mi.PageIsPublished }).ToList();
					}
				}
				if (model.MenuItems != null) {
					foreach (var m in model.MenuItems) {
						var response1 = api.Pages.Page.Translations.Get(new BetterCms.Module.Api.Operations.Pages.Pages.Page.Translations.GetPageTranslationsRequest() { PageId = m.PageId });
						if (response1.Data.TotalCount > 0) {
							m.Translations = response1.Data.Items.Select(x => new MenuTranslationItem() { IsPublished = x.IsPublished, LanguageCode = x.LanguageCode ?? defaultLanguageCode, Url = x.PageUrl }).ToList();
						}
					}

				}
			}

			return View("Index2", model);
		}

		private Guid? GetLanguageId(IApiFacade api, string languageCode)
		{
			if (string.IsNullOrEmpty(languageCode))
			{
				return null;
			}

			try
			{
				var request = new GetLanguageRequest { LanguageCode = languageCode };
				var response = api.Root.Language.Get(request);
				return response.Data.Id;
			}
			catch
			{
			}

			return Guid.Empty;
		}

		private Guid? GetSitemapId(IApiFacade api, Guid defaultValue)
		{
			var allSitemaps = api.Pages.Sitemaps.Get(new GetSitemapsRequest());
			if (allSitemaps.Data.Items.Count > 0)
			{
				var sitemap = allSitemaps.Data.Items.FirstOrDefault(map => map.Id == defaultValue) ?? allSitemaps.Data.Items.First();
				return sitemap.Id;
			}

			return null;
		}
	}
}