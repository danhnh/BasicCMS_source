using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class RoomInfo : EquatableEntity<RoomInfo>, IUserChoice {
		public virtual Room Room { get; set; }
		public virtual Classification Classification { get; set; }
		public virtual string SchemeCode { get; set; }
		public virtual string Description { get; set; }
		public virtual bool IsSelected { get; set; }
	}
}