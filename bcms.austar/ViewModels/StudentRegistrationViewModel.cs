using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class StudentRegistrationViewModel : IRegistrationViewModel {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string Yourself { get; set; }
		public string Family { get; set; }
		public string FavouriteFood { get; set; }
		public string TravelInsuranceProvider { get; set; }
		public string TravelInsuranceNumber { get; set; }
		public DateTime? TravelInsuranceExpiryDate { get; set; }
		public DateTime? ArrivalDate { get; set; }
		public string ArrivalTime { get; set; }
		public string Airline { get; set; }
		public string FlightNumber { get; set; }
		public string HostPreferenceReason { get; set; }
		public string HostOtherPreferences { get; set; }
		public string College { get; set; }
		public string Campus { get; set; }
		public string CollegeAddress { get; set; }
		public List<Models.StudentLanguage> Languages { get; set; }
		public StudentRegistrationViewModel(Models.StudentRegistration register) {
			this.Yourself = register.Yourself;
			this.TravelInsuranceExpiryDate = register.TravelInsuranceExpiryDate;
			this.ArrivalDate = register.ArrivalDate;
			this.Family = register.Family;
			this.FavouriteFood = register.FavouriteFood;
			this.TravelInsuranceProvider = register.TravelInsuranceProvider;
			this.TravelInsuranceNumber = register.TravelInsuranceNumber;
			this.ArrivalTime = register.ArrivalTime;
			this.Airline = register.Airline;
			this.FlightNumber = register.FlightNumber;
			this.HostPreferenceReason = register.HostPreferenceReason;
			this.HostOtherPreferences = register.HostOtherPreferences;
			this.College = register.College;
			this.Campus = register.Campus;
			this.CollegeAddress = register.CollegeAddress;
		}
		public StudentRegistrationViewModel() { }
		public string GetClassificationInfoValue(string schemeCode, int index = -1) {
			string val = "";
			if (this.Choices.Count(x => x.SchemeCode.ToLower().Equals(schemeCode.ToLower())) > 0)
				foreach (string s in this.Choices.Where(x => x.IsSelected && x.SchemeCode.ToLower().Equals(schemeCode.ToLower()))
					.Select(x => x.Description))
					val += s;
			return val;
		}
		public Models.StudentLanguage GetLanguage(int languageIndex) {
			return Languages.FirstOrDefault(x => x.LanguageIndex == languageIndex);
		}
	}
}