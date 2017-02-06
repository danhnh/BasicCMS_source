using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611102000)]
	public class AddClassifications : DefaultMigration {
		public AddClassifications()
			: base(AustarModuleDescriptor.ModuleName) {
		}

		public override void Up() {
			CreateClassificationsTable();
			InsertDefaultData();
		}
		public override void Down() {
			throw new NotImplementedException();
		}
		private void CreateClassificationsTable() {
			Create
				.Table("Classifications")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("Name").AsString(MaxLength.Name).NotNullable()
				.WithColumn("Description").AsString(MaxLength.Text).NotNullable()
				.WithColumn("SchemeCode").AsString(MaxLength.Name).NotNullable()
				.WithColumn("SortOrder").AsInt32().Nullable()
				.WithColumn("RequireMoreInfo").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("MoreInfoDescription").AsString(MaxLength.Text).Nullable();
		}

		private void InsertDefaultData() {
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.Empty,
					Name = "Other...",
					Description = "Other...",
					SchemeCode = "",
					SortOrder = 0,
					RequireMoreInfo = true,
					MoreInfoDescription = "Other:",
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = new Guid("D04ECC15-01F7-4940-B4AA-12F247C2CB1E"),
					Name = "Male",
					Description = "Male",
					SchemeCode = "GENDER",
					SortOrder = 1,
					RequireMoreInfo = false,
					MoreInfoDescription = "",
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = new Guid("51202FD8-5134-4B06-BB0F-DC5FF135AD39"),
					Name = "Female",
					Description = "Female",
					SchemeCode = "GENDER",
					RequireMoreInfo = false,
					MoreInfoDescription = "",
					SortOrder = 2,
					Version = "1",
					CreatedByUser = "Admin CMS",
					ModifiedByUser = "Admin CMS"
				});
		}
	}
}