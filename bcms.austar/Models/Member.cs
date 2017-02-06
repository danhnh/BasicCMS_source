using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class Member : EquatableEntity<Member> {
		public virtual HostRegistration HostRegistration { get; set; }
		public virtual int MemberIndex { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string Occupation { get; set; }
		public virtual bool HaveWWCC { get; set; }
		public virtual string ClearanceNumber { get; set; }
		public virtual DateTime? ClearanceExpiryDate { get; set; }
		public virtual byte[] WWCCScannedCopy { get; set; }

	}
}