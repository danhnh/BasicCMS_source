using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class ClassificationMap : EntityMapBase<Classification> {
		public ClassificationMap() : base(AustarModuleDescriptor.ModuleName) {
			Table("Classifications");
			Map(x => x.Name).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.Description).Not.Nullable().Length(MaxLength.Text);
			Map(x => x.SchemeCode).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.Group).Not.Nullable().Length(MaxLength.Name).Column("[Group]");
			Map(x => x.SortOrder).Nullable();
			Map(x => x.RequireMoreInfo).Not.Nullable();
			Map(x => x.MoreInfoDescription).Nullable().Length(MaxLength.Text);
			Map(x => x.IsMultiChoice).Not.Nullable();
		}
	}
}