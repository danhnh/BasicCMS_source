using bcms.austar.ViewModels;
using bcms.austar.Models;
using BetterCms.Module.Root.Mvc;
using BetterModules.Core.Web.Mvc.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Commands {
	public class DeleteUniversityCommand : CommandBase, ICommand<UniversityViewModel, bool> {
		public bool Execute(UniversityViewModel request) {
			var university = Repository.Delete<University>(request.Id, request.Version);
			UnitOfWork.Commit();

			BetterCms.Events.UniversityEvents.Instance.OnUniversityDeleted(university);

			return true;
		}
	}
}