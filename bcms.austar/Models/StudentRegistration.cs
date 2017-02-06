using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class StudentRegistration : EquatableEntity<StudentRegistration> {
		public virtual BetterCms.Module.Users.Models.User User { get; set; }
		public virtual Classification Gender { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string HomePhone { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual string PassportNumber { get; set; }
		public virtual DateTime? PassportExpiryDate { get; set; }
		public virtual string Yourself { get; set; }
		public virtual string Family { get; set; }
		public virtual string FavouriteFood { get; set; }
		public virtual string TravelInsuranceProvider { get; set; }
		public virtual string TravelInsuranceNumber { get; set; }
		public virtual DateTime? TravelInsuranceExpiryDate { get; set; }
		public virtual byte[] TravelInsuranceScannedCopy { get; set; }
		public virtual DateTime? ArrivalDate { get; set; }
		public virtual string ArrivalTime { get; set; }
		public virtual string Airline { get; set; }
		public virtual string FlightNumber { get; set; }
		public virtual string HostPreferenceReason { get; set; }
		public virtual string HostOtherPreferences { get; set; }
		public virtual string College { get; set; }
		public virtual string Campus { get; set; }
		public virtual string CollegeAddress { get; set; }
	}
}