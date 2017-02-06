using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class UserChoice : EquatableEntity<UserChoice>, IUserChoice {
		public virtual BetterCms.Module.Users.Models.User User { get; set; }
		public virtual Classification Classification { get; set; }
		public virtual string SchemeCode { get; set; }
		public virtual string Description { get; set; }
		public virtual bool IsSelected { get; set; }
	}
}