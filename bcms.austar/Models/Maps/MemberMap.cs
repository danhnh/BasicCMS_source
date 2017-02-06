using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class MemberMap : EntityMapBase<Member> {
		public MemberMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("Members");
			References(x => x.HostRegistration).Cascade.SaveUpdate().LazyLoad();
			
			Map(x => x.MemberIndex).Not.Nullable();

			Map(x => x.FirstName).Nullable().Length(MaxLength.Name);
			Map(x => x.LastName).Nullable().Length(MaxLength.Name);
			Map(x => x.BirthDate).Nullable();

			Map(x => x.HaveWWCC).Not.Nullable();

			Map(x => x.Occupation).Nullable().Length(MaxLength.Text);
			Map(x => x.ClearanceNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.ClearanceExpiryDate).Nullable();
			Map(x => x.WWCCScannedCopy).Nullable();
		}
	}
}