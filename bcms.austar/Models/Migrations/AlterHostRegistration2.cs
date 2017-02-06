using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611111100)]
	public class AlterHostRegistration2 : DefaultMigration {
		public AlterHostRegistration2() : base(AustarModuleDescriptor.ModuleName) {
			
		}
		public override void Up() {
			AddHostRegistrationsPostalAddress();
		}

		private void AddHostRegistrationsPostalAddress() {
			Alter
				.Table("HostRegistrations")
				.InSchema(SchemaName)
				.AlterColumn("HomeUnitNbr").AsString(50).Nullable()
				.AlterColumn("HomeStreetNbr").AsString(50).Nullable()
				.AlterColumn("HomeStreetName").AsString(MaxLength.Name).Nullable()
				.AlterColumn("HomeCity").AsString(MaxLength.Name).Nullable()
				.AlterColumn("HomePostCode").AsString(10).Nullable()
				.AlterColumn("HomeState").AsString(10).Nullable()
				.AlterColumn("PostalAddress").AsString(MaxLength.Text).Nullable()
				.AlterColumn("PostalUnitNbr").AsString(50).Nullable()
				.AlterColumn("PostalStreetNbr").AsString(50).Nullable()
				.AlterColumn("PostalStreetName").AsString(MaxLength.Name).Nullable()
				.AlterColumn("PostalCity").AsString(MaxLength.Name).Nullable()
				.AlterColumn("PostalPostCode").AsString(10).Nullable()
				.AlterColumn("PostalState").AsString(10).Nullable();
		}
	}
}