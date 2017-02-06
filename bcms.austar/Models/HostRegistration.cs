using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class HostRegistration : EquatableEntity<HostRegistration> {
		public virtual BetterCms.Module.Users.Models.User User { get; set; }
		public virtual Classification Gender { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string Occupation { get; set; }
		public virtual string HomePhone { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual string HomeAddress { get; set; }
		public virtual string HomeUnitNbr { get; set; }
		public virtual string HomeStreetNbr { get; set; }
		public virtual string HomeStreetName { get; set; }
		public virtual string HomeCity { get; set; }
		public virtual string HomeState { get; set; }
		public virtual string HomePostCode { get; set; }
		public virtual bool PostalSameAsHome { get; set; }
		public virtual string PostalAddress { get; set; }
		public virtual string PostalUnitNbr { get; set; }
		public virtual string PostalStreetNbr { get; set; }
		public virtual string PostalStreetName { get; set; }
		public virtual string PostalCity { get; set; }
		public virtual string PostalState { get; set; }
		public virtual string PostalPostCode { get; set; }
		public virtual string Family { get; set; }
		public virtual string Home { get; set; }
		public virtual string BankAccountName { get; set; }
		public virtual string BankAccountNumber { get; set; }
		public virtual string BankName { get; set; }
		public virtual string BankBSB { get; set; }
		public virtual string PLInsuranceProvider { get; set; }
		public virtual string PLInsuranceNumber { get; set; }
		public virtual DateTime? PLInsuranceExpiryDate { get; set; }
		public virtual string HCInsuranceProvider { get; set; }
		public virtual string HCInsuranceNumber { get; set; }
		public virtual DateTime? HCInsuranceExpiryDate { get; set; }

		public virtual string StudentPreferenceReason { get; set; }
		public virtual string StudentOtherPreferences { get; set; }

		public virtual string HomeComments { get; set; }
		public virtual string OtherAboutPets { get; set; }
		public virtual string EmergencyContact { get; set; }
		public virtual string LifestyleComments { get; set; }
		public virtual string WillHostNationality { get; set; }
		public virtual string ServiceComments { get; set; }

		public virtual string ExtraEmails { get; set; }
		public virtual string GuestWellcomeMessage { get; set; }
		public virtual string PublicProfile { get; set; }
		public virtual string AdditionalCosts { get; set; }
		public virtual string HouseholdOccupations { get; set; }
		public virtual string PublicComments { get; set; }







	}
}