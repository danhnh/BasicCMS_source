using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611061500)]
	public class InitialSetup : DefaultMigration {
		/// <summary>
		/// The root module schema name.
		/// </summary>
		//private readonly string rootModuleSchemaName;

		/// <summary>
		/// The media manager schema name.
		/// </summary>
		//private readonly string mediaManagerSchemaName;
		public InitialSetup()
			: base(AustarModuleDescriptor.ModuleName) {
			//rootModuleSchemaName = (new BetterCms.Module.Root.Models.Migrations.RootVersionTableMetaData()).SchemaName;
			//mediaManagerSchemaName = (new BetterCms.Module.MediaManager.Models.Migrations.MediaManagerVersionTableMetaData()).SchemaName;
		}

		public override void Up() {
			CreateUniversitiesTable();
			InsertDefaultData();
		}
		public override void Down() {
			throw new NotImplementedException();
		}

		private void CreateUniversitiesTable() {
			Create
				.Table("Universities")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("Name").AsString(MaxLength.Name).NotNullable()
				.WithColumn("ShortName").AsString(MaxLength.Name).NotNullable()
				.WithColumn("StreetAddress").AsString(MaxLength.Text).Nullable()
				.WithColumn("PostCode").AsString(10).NotNullable()
				.WithColumn("WebsiteUrl").AsString(MaxLength.Text).Nullable()
				.WithColumn("State").AsString(10).NotNullable();
		}

		private void InsertDefaultData() {
			Insert.IntoTable("Universities")
				.InSchema(SchemaName)
				.Row(new {
					Name = "The Australian National University",
					ShortName = "ANU",
					StreetAddress = "East Road, ACTON, ACT 2601",
					PostCode = "2601",
					WebsiteUrl = "http://www.anu.edu.au/",
					State = "ACT",
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
		}
	}
}