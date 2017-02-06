using BetterModules.Core.Models;


namespace bcms.austar.Models.Maps
{
	public class UniversityMap : EntityMapBase<University>
	{
		public UniversityMap() : base(AustarModuleDescriptor.ModuleName)
		{
			Table("Universities");
			Map(x => x.Name).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.ShortName).Not.Nullable().Length(MaxLength.Name);
			Map(x => x.StreetAddress).Nullable().Length(MaxLength.Text);
			Map(x => x.PostCode).Not.Nullable().Length(10);
			Map(x => x.WebsiteUrl).Nullable().Length(MaxLength.Text);
			Map(x => x.State).Nullable().Length(10);
		}
	}
}