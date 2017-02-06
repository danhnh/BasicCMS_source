using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class HostRegistrationStep2 {
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
	}
	
}