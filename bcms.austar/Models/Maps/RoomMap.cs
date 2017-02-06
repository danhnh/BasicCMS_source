using BetterModules.Core.Models;

namespace bcms.austar.Models.Maps {
	public class RoomMap : EntityMapBase<Room> {
		public RoomMap(): base(AustarModuleDescriptor.ModuleName) {
			Table("Rooms");
			References(x => x.HostRegistration).Cascade.SaveUpdate().LazyLoad();
			Map(x => x.RoomType).Not.Nullable();
			Map(x => x.RoomIndex).Not.Nullable();

			Map(x => x.RoomNo).Nullable().Length(MaxLength.Name);
			Map(x => x.AvailableFrom).Nullable();
			Map(x => x.AvailableTo).Nullable();
			Map(x => x.DateLeaving).Nullable();
		}
	}
}