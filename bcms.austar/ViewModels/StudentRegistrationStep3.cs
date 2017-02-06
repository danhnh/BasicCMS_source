using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class StudentRegistrationStep3 {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string HostPreferenceReason { get; set; }
		public string HostOtherPreferences { get; set; }
		public string College { get; set; }
		public string Campus { get; set; }
		public string CollegeAddress { get; set; }
	}
}