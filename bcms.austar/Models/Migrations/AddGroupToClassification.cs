using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611132000)]
	public class AddGroupToClassification : DefaultMigration {
		public AddGroupToClassification()
			: base(AustarModuleDescriptor.ModuleName) {
		}

		public override void Up() {
			AddGroupToClassificationTable();
		}
		public override void Down() {
			throw new NotImplementedException();
		}
		private void AddGroupToClassificationTable() {
			Alter
				.Table("Classifications")
				.InSchema(SchemaName)
				.AddColumn("Group").AsString(MaxLength.Name).NotNullable().WithDefaultValue("");
		}
	}
}