using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class HostRegistrationStep5 {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }

		public string ExtraEmails { get; set; }
		public string GuestWellcomeMessage { get; set; }
		public string PublicProfile { get; set; }
		public string AdditionalCosts { get; set; }
		public string HouseholdOccupations { get; set; }
		public string PublicComments { get; set; }
	}
}