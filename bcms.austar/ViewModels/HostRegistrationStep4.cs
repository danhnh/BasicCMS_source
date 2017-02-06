using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class HostRegistrationStep4 {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string StudentPreferenceReason { get; set; }
		public string StudentOtherPreferences { get; set; }
		public string WillHostNationality { get; set; }
		public string ServiceComments { get; set; }
	}
}