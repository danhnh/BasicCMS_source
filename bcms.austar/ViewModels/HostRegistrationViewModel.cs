using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public interface IRegistrationViewModel {
		string GetClassificationInfoValue(string schemeCode, int index = -1);
	}
	public class HostRegistrationViewModel : IRegistrationViewModel {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string PLInsuranceProvider { get; set; }
		public string PLInsuranceNumber { get; set; }
		public DateTime? PLInsuranceExpiryDate { get; set; }
		public string HCInsuranceProvider { get; set; }
		public string HCInsuranceNumber { get; set; }
		public DateTime? HCInsuranceExpiryDate { get; set; }
		public string HomeComments { get; set; }
		public string Family { get; set; }
		public string Home { get; set; }
		public string OtherAboutPets { get; set; }
		public string EmergencyContact { get; set; }
		public string LifestyleComments { get; set; }
		public string StudentPreferenceReason { get; set; }
		public string StudentOtherPreferences { get; set; }
		public string WillHostNationality { get; set; }
		public string ServiceComments { get; set; }
		public string ExtraEmails { get; set; }
		public string GuestWellcomeMessage { get; set; }
		public string PublicProfile { get; set; }
		public string AdditionalCosts { get; set; }
		public string HouseholdOccupations { get; set; }
		public string PublicComments { get; set; }
		public string BankAccountName { get; set; }
		public string BankAccountNumber { get; set; }
		public string BankName { get; set; }
		public string BankBSB { get; set; }
		public List<Models.RoomInfo> Rooms { get; set; }
		public List<Models.MemberInfo> Members { get; set; }
		public List<Models.MemberLanguage> MemberLanguages { get; set; }
		public HostRegistrationViewModel(Models.HostRegistration register) {
			this.PLInsuranceProvider = register.PLInsuranceProvider;
			this.PLInsuranceNumber = register.PLInsuranceNumber;
			this.PLInsuranceExpiryDate = register.PLInsuranceExpiryDate;

			this.HCInsuranceProvider = register.HCInsuranceProvider;
			this.HCInsuranceNumber = register.HCInsuranceNumber;
			this.HCInsuranceExpiryDate = register.HCInsuranceExpiryDate;

			this.HomeComments = register.HomeComments;
			this.Family = register.Family;
			this.Home = register.Home;
			this.OtherAboutPets = register.OtherAboutPets;
			this.EmergencyContact = register.EmergencyContact;
			this.LifestyleComments = register.LifestyleComments;
			this.StudentPreferenceReason = register.StudentPreferenceReason;
			this.StudentOtherPreferences = register.StudentOtherPreferences;
			this.WillHostNationality = register.WillHostNationality;
			this.ServiceComments = register.ServiceComments;
			this.ExtraEmails = register.ExtraEmails;
			this.GuestWellcomeMessage = register.GuestWellcomeMessage;
			this.PublicProfile = register.PublicProfile;
			this.AdditionalCosts = register.AdditionalCosts;
			this.HouseholdOccupations = register.HouseholdOccupations;
			this.PublicComments = register.PublicComments;
			this.BankAccountName = register.BankAccountName;
			this.BankAccountNumber = register.BankAccountNumber;
			this.BankName = register.BankName;
			this.BankBSB = register.BankBSB;
		}

		public string GetClassificationInfoValue(string schemeCode, int index = -1) {
			string val = "";
			if(this.Choices.Count(x=> x.SchemeCode.ToLower().Equals(schemeCode.ToLower())) > 0)
				foreach (string s in this.Choices.Where(x => x.IsSelected && x.SchemeCode.ToLower().Equals(schemeCode.ToLower()))
					.Select(x => x.Description))
					val += s;
			else if (index > -1) {
				if (this.Rooms.Count(x => x.SchemeCode.ToLower().Equals(schemeCode.ToLower())) > 0) {
				foreach (string s in this.Rooms.Where(x => x.Room.RoomIndex == index && x.IsSelected && x.SchemeCode.ToLower().Equals(schemeCode.ToLower()))
					.Select(x => x.Description))
						val += s;
				}
				if (this.Members.Count(x => x.SchemeCode.ToLower().Equals(schemeCode.ToLower())) > 0) {
					foreach (string s in this.Members.Where(x => x.Member.MemberIndex == index && x.IsSelected && x.SchemeCode.ToLower().Equals(schemeCode.ToLower()))
						.Select(x => x.Description))
						val += s;
				}
			}
			return val;
		}

		public Models.RoomInfo GetBedRoom(int index) {
			return Rooms.FirstOrDefault(x => x.Room.RoomType == 1 && x.Room.RoomIndex == index);
		}
		public Models.RoomInfo GetBathRoom(int index) {
			return Rooms.FirstOrDefault(x => x.Room.RoomType == 2 && x.Room.RoomIndex == index);
		}
		public Models.MemberInfo GetMember(int index) {
			return Members.FirstOrDefault(x => x.Member.MemberIndex == index);
		}
		public Models.MemberLanguage GetMemberLanguage(int memberIndex, int languageIndex) {
			return MemberLanguages.FirstOrDefault(x => x.Member.MemberIndex == memberIndex && x.LanguageIndex == languageIndex);
		}
	}
	
}