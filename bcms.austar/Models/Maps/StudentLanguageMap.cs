using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class StudentLanguageMap : EntityMapBase<StudentLanguage> {
		public StudentLanguageMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("StudentLanguages");
			References(x => x.Student).Cascade.SaveUpdate().LazyLoad();

			Map(x => x.LanguageIndex).Not.Nullable();

			References(x => x.Language).Nullable().Cascade.SaveUpdate().LazyLoad();
			References(x => x.Proficiency).Nullable().Cascade.SaveUpdate().LazyLoad();
		}
	}
}