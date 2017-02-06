using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class HostRegistrationMap : EntityMapBase<HostRegistration> {
		public HostRegistrationMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("HostRegistrations");
			References(x => x.User).Cascade.SaveUpdate().LazyLoad();
			References(x => x.Gender).Cascade.SaveUpdate().LazyLoad();

			Map(x => x.BirthDate).Nullable();
			Map(x => x.Occupation).Nullable().Length(MaxLength.Text);
			Map(x => x.HomePhone).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.MobilePhone).Nullable().Length(MaxLength.Name);
			Map(x => x.HomeAddress).Not.Nullable().Length(MaxLength.Text);
			Map(x => x.HomeUnitNbr).Nullable().Length(50);
			Map(x => x.HomeStreetNbr).Nullable().Length(50);
			Map(x => x.HomeStreetName).Nullable().Length(MaxLength.Name);
			Map(x => x.HomeCity).Nullable().Length(MaxLength.Name);
			Map(x => x.HomeState).Nullable().Length(10);
			Map(x => x.HomePostCode).Nullable().Length(10);
			Map(x => x.PostalSameAsHome).Not.Nullable();
			Map(x => x.PostalAddress).Nullable().Length(MaxLength.Text);
			Map(x => x.PostalUnitNbr).Nullable().Length(50);
			Map(x => x.PostalStreetNbr).Nullable().Length(50);
			Map(x => x.PostalStreetName).Nullable().Length(MaxLength.Name);
			Map(x => x.PostalCity).Nullable().Length(MaxLength.Name);
			Map(x => x.PostalState).Nullable().Length(10);
			Map(x => x.PostalPostCode).Nullable().Length(10);
			Map(x => x.Family).Nullable().Length(MaxLength.Text);
			Map(x => x.Home).Nullable().Length(MaxLength.Text);

			Map(x => x.BankAccountName).Nullable().Length(MaxLength.Text);
			Map(x => x.BankAccountNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.BankName).Nullable().Length(MaxLength.Text);
			Map(x => x.BankBSB).Nullable().Length(MaxLength.Text);
			Map(x => x.PLInsuranceProvider).Nullable().Length(MaxLength.Text);
			Map(x => x.PLInsuranceNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.PLInsuranceExpiryDate).Nullable();
			Map(x => x.HCInsuranceProvider).Nullable().Length(MaxLength.Text);
			Map(x => x.HCInsuranceNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.HCInsuranceExpiryDate).Nullable();

			Map(x => x.StudentPreferenceReason).Nullable().Length(MaxLength.Text);
			Map(x => x.StudentOtherPreferences).Nullable().Length(MaxLength.Text);
			
			Map(x => x.HomeComments ).Nullable().Length(MaxLength.Text);
			Map(x => x.OtherAboutPets ).Nullable().Length(MaxLength.Text);
			Map(x => x.EmergencyContact ).Nullable().Length(MaxLength.Text);
			Map(x => x.LifestyleComments ).Nullable().Length(MaxLength.Text);
			Map(x => x.WillHostNationality ).Nullable().Length(MaxLength.Text);
			Map(x => x.ServiceComments ).Nullable().Length(MaxLength.Text);
			Map(x => x.ExtraEmails ).Nullable().Length(MaxLength.Text);
			Map(x => x.GuestWellcomeMessage ).Nullable().Length(MaxLength.Text);
			Map(x => x.PublicProfile ).Nullable().Length(MaxLength.Text);
			Map(x => x.AdditionalCosts ).Nullable().Length(MaxLength.Text);
			Map(x => x.HouseholdOccupations ).Nullable().Length(MaxLength.Text);
			Map(x => x.PublicComments ).Nullable().Length(MaxLength.Text);
		}
	}
}