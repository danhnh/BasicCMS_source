using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class MemberInfoMap : EntityMapBase<MemberInfo> {
		public MemberInfoMap() : base(AustarModuleDescriptor.ModuleName) {
			Table("MemberInfoes");
			References(x => x.Member).Cascade.SaveUpdate().LazyLoad();
			References(x => x.Classification).Cascade.SaveUpdate().LazyLoad();

			Map(x => x.SchemeCode).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.Description).Not.Nullable().Length(MaxLength.Text);

			Map(x => x.IsSelected).Not.Nullable();
		}
	}
}