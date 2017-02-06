using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class StudentRegistrationMap : EntityMapBase<StudentRegistration> {
		public StudentRegistrationMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("StudentRegistrations");
			References(x => x.User).Cascade.SaveUpdate().LazyLoad();
			References(x => x.Gender).Cascade.SaveUpdate().LazyLoad();
			Map(x => x.BirthDate).Nullable();
			Map(x => x.HomePhone).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.MobilePhone).Nullable().Length(MaxLength.Name);
			Map(x => x.Yourself).Nullable().Length(MaxLength.Text);
			Map(x => x.Family).Nullable().Length(MaxLength.Text);
			Map(x => x.FavouriteFood).Nullable().Length(MaxLength.Text);
			Map(x => x.TravelInsuranceProvider).Nullable().Length(MaxLength.Text);
			Map(x => x.TravelInsuranceNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.TravelInsuranceExpiryDate).Nullable();
			Map(x => x.TravelInsuranceScannedCopy).Nullable();
			Map(x => x.ArrivalDate).Nullable();
			Map(x => x.ArrivalTime).Nullable().Length(MaxLength.Name);
			Map(x => x.Airline).Nullable().Length(MaxLength.Name);
			Map(x => x.FlightNumber).Nullable().Length(MaxLength.Name);
			Map(x => x.HostPreferenceReason).Nullable().Length(MaxLength.Text);
			Map(x => x.HostOtherPreferences).Nullable().Length(MaxLength.Text);
			Map(x => x.College).Nullable().Length(MaxLength.Text);
			Map(x => x.Campus).Nullable().Length(MaxLength.Text);
			Map(x => x.CollegeAddress).Nullable().Length(MaxLength.Text);
			Map(x => x.PassportNumber).Nullable().Length(MaxLength.Text);
			Map(x => x.PassportExpiryDate).Nullable();
		}

	}
}