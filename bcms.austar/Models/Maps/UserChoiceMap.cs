using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class UserChoiceMap : EntityMapBase<UserChoice> {
		public UserChoiceMap() : base(AustarModuleDescriptor.ModuleName){
			Table("UserChoices");
			References(x => x.User).Cascade.SaveUpdate().LazyLoad();
			References(x => x.Classification).Cascade.SaveUpdate().LazyLoad();

			Map(x => x.SchemeCode).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.Description).Not.Nullable().Length(MaxLength.Text);
			
			Map(x => x.IsSelected).Not.Nullable();
		}
	}
}