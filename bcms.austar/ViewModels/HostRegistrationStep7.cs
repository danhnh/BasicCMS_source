using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.ViewModels {
	public class HostRegistrationStep7 {
		public Guid Id { get; set; }
		public List<Models.UserChoice> Choices { get; set; }
		public List<Models.Classification> Classifications { get; set; }
		public string BankAccountName { get; set; }
		public string BankAccountNumber { get; set; }
		public string BankName { get; set; }
		public string BankBSB { get; set; }
	}
}