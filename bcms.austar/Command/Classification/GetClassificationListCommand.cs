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
using bcms.austar.Models;

namespace bcms.austar.Command.Classification {
	public class GetClassificationListCommand : CommandBase, ICommand<string, List<Models.Classification>> {
		public List<Models.Classification> Execute(string request) {
			var query = Repository.AsQueryable<Models.Classification>();
			if (!string.IsNullOrWhiteSpace(request)) {
				request = request.ToLower();
				query = query.Where(a => "".Equals(a.SchemeCode) || a.SchemeCode.ToLower().Equals(request));
			}
			return query.ToList();
		}
	}
}