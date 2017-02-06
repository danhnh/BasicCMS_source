using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class Room : EquatableEntity<Room> {
		public virtual HostRegistration HostRegistration { get; set; }
		public virtual int RoomType { get; set; }
		public virtual int RoomIndex { get; set; }
		public virtual string RoomNo { get; set; }
		public virtual DateTime? AvailableFrom { get; set; }
		public virtual DateTime? AvailableTo { get; set; }
		public virtual DateTime? DateLeaving { get; set; }
	}
}