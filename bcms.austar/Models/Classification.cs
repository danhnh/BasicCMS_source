using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class Classification : EquatableEntity<Classification> {
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string SchemeCode { get; set; }
		public virtual string Group { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual bool RequireMoreInfo { get; set; }
		public virtual string MoreInfoDescription { get; set; }
		public virtual bool IsMultiChoice { get; set; }
	}
}