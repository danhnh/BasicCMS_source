using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201612312350)]
	public class AlterHostRegistration4 : DefaultMigration {
		public AlterHostRegistration4()
			: base(AustarModuleDescriptor.ModuleName) {
		}

		public override void Up() {
			AlterHostRegistration();
			
		}
		public override void Down() {
			throw new NotImplementedException();
		}
		private void AlterHostRegistration() {
			Alter
				.Table("HostRegistrations")
				.InSchema(SchemaName)
				.AddColumn("HomeComments").AsString(MaxLength.Text).Nullable()
				.AddColumn("OtherAboutPets").AsString(MaxLength.Text).Nullable()
				.AddColumn("EmergencyContact").AsString(MaxLength.Text).Nullable()
				.AddColumn("LifestyleComments").AsString(MaxLength.Text).Nullable()
				.AddColumn("WillHostNationality").AsString(MaxLength.Text).Nullable()
				.AddColumn("ServiceComments").AsString(MaxLength.Text).Nullable()

				.AddColumn("ExtraEmails").AsString(MaxLength.Text).Nullable()
				.AddColumn("GuestWellcomeMessage").AsString(MaxLength.Text).Nullable()
				.AddColumn("PublicProfile").AsString(MaxLength.Text).Nullable()
				.AddColumn("AdditionalCosts").AsString(MaxLength.Text).Nullable()
				.AddColumn("HouseholdOccupations").AsString(MaxLength.Text).Nullable()
				.AddColumn("PublicComments").AsString(MaxLength.Text).Nullable();
		}

	}
}