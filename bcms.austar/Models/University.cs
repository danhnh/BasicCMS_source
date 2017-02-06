using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class University : EquatableEntity<University> {
		public virtual string Name { get; set; }
		public virtual string ShortName { get; set; }
		public virtual string StreetAddress { get; set; }
		public virtual string PostCode { get; set; }
		public virtual string WebsiteUrl { get; set; }
		public virtual string State { get; set; }
	}
}