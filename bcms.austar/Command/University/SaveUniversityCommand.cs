
using bcms.austar.Services;
using bcms.austar.ViewModels;
using BetterCms.Module.Root.Mvc;

using BetterModules.Core.Web.Mvc.Commands;

namespace bcms.austar.Commands {
	public class SaveUniversityCommand : CommandBase, ICommand<UniversityViewModel, UniversityViewModel> {
		public IUniversityService UniversityService { get; set; }
		public UniversityViewModel Execute(UniversityViewModel request) {
			var university = UniversityService.SaveUniversity(request.Name, request.ShortName, request.Id, request.Version);

			return new UniversityViewModel {
				Id = university.Id,
				Version = university.Version,
				Name = university.Name,
				ShortName = university.ShortName//,
				//PostCode = university.PostCode,
				//StreetAddress = university.StreetAddress
			};
		}
	}
}