using bcms.austar.ViewModels;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;
using BetterModules.Core.DataAccess.DataContext;
using BetterModules.Core.Web.Mvc.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Commands {
	public class GetUniversityListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<UniversityViewModel>> {
		public SearchableGridViewModel<UniversityViewModel> Execute(SearchableGridOptions request) {
			request.SetDefaultSortingOptions("Name");
			var query = Repository.AsQueryable<Models.University>();
			if (!string.IsNullOrWhiteSpace(request.SearchQuery)) {
				query = query.Where(a => a.Name.Contains(request.SearchQuery));
			}
			var universities = query
				.Select(uni =>
					new UniversityViewModel {
						Id = uni.Id,
						Version = uni.Version,
						Name = uni.Name,
						ShortName = uni.ShortName,
						StreetAddress = uni.StreetAddress,
						PostCode = uni.PostCode,
						WebsiteUrl = uni.WebsiteUrl
					});

			var count = query.ToRowCountFutureValue();
			universities = universities.AddSortingAndPaging(request);
			return new SearchableGridViewModel<UniversityViewModel>(universities.ToList(), request, count.Value);
		}
	}
}