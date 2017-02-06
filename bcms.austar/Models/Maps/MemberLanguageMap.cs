using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class MemberLanguageMap : EntityMapBase<MemberLanguage> {
		public MemberLanguageMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("MemberLanguages");
			References(x => x.Member).Cascade.SaveUpdate().LazyLoad();

			Map(x => x.LanguageIndex).Not.Nullable();

			References(x => x.Language).Nullable().Cascade.SaveUpdate().LazyLoad();
			References(x => x.Proficiency).Nullable().Cascade.SaveUpdate().LazyLoad();
		}
	}
}