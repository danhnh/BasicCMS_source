using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;


namespace bcms.austar.Models.Migrations {
	[Migration(201611111700)]
	public class InstallNewUserRole: DefaultMigration {
		public InstallNewUserRole()
			: base(AustarModuleDescriptor.ModuleName)
		{
			//rootModuleSchemaName = (new BetterCms.Module.Root.Models.Migrations.RootVersionTableMetaData()).SchemaName;
			//mediaManagerSchemaName = (new BetterCms.Module.MediaManager.Models.Migrations.MediaManagerVersionTableMetaData()).SchemaName;
		}
		public override void Up() {
			Insert.IntoTable("Roles")
				.InSchema("bcms_users")
				.Row(new {
					Name = "AustarHost",
					Description = "Austar Host",
					IsSystematic = false,
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Roles")
				.InSchema("bcms_users")
				.Row(new {
					Name = "AustarStudent",
					Description = "Austar Student",
					IsSystematic = false,
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
		}
		public override void Down() {
			throw new NotImplementedException();
		}
	}
}