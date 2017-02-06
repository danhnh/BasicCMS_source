using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class HostRegistrationStep3 {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string Family { get; set; }
		public string Home { get; set; }
		public string OtherAboutPets { get; set; }
		public string EmergencyContact { get; set; }
		public string LifestyleComments { get; set; }
	}
}