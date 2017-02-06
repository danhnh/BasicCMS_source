using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611141030)]
	public class AlterStudentRegistration2 : DefaultMigration {
		public AlterStudentRegistration2() : base(AustarModuleDescriptor.ModuleName) {
			
		}
		public override void Up() {
			Alter
				.Table("StudentRegistrations")
				.InSchema(SchemaName)
				.AddColumn("HomePhone").AsString(MaxLength.Name).NotNullable()
				.AddColumn("MobilePhone").AsString(MaxLength.Name).Nullable();
		}
	}
}