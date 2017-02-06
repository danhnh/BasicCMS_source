using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201701011000)]
	public class AlterLanguage : DefaultMigration {
		public AlterLanguage() : base(AustarModuleDescriptor.ModuleName) {
			
		}
		public override void Up() {
			Alter
				.Table("MemberLanguages")
				.InSchema(SchemaName)
				.AlterColumn("LanguageId").AsGuid().Nullable()
				.AlterColumn("ProficiencyId").AsGuid().Nullable();

			Alter
				.Table("StudentLanguages")
				.InSchema(SchemaName)
				.AlterColumn("LanguageId").AsGuid().Nullable()
				.AlterColumn("ProficiencyId").AsGuid().Nullable();
		}
	}
}