using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611110900)]
	public class AlterHostRegistration1 : DefaultMigration {
		public AlterHostRegistration1() : base(AustarModuleDescriptor.ModuleName) {
			
		}
		public override void Up() {
			AddHostRegistrationsPostalAddress();
		}

		private void AddHostRegistrationsPostalAddress() {
			Alter
				.Table("HostRegistrations")
				.InSchema(SchemaName)
				.AddColumn("PostalSameAsHome").AsBoolean().NotNullable().WithDefaultValue(true)
				.AddColumn("PostalAddress").AsString(MaxLength.Text).NotNullable()
				.AddColumn("PostalUnitNbr").AsString(50).Nullable()
				.AddColumn("PostalStreetNbr").AsString(50).Nullable()
				.AddColumn("PostalStreetName").AsString(MaxLength.Name).Nullable()
				.AddColumn("PostalCity").AsString(MaxLength.Name).Nullable()
				.AddColumn("PostalPostCode").AsString(10).NotNullable()
				.AddColumn("PostalState").AsString(10).NotNullable();
		}
	}
}