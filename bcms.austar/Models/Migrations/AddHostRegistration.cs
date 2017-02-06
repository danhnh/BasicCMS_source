using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611102300)]
	public class AddHostRegistration : DefaultMigration {
		private readonly string usersModuleSchemaName;
		public AddHostRegistration() : base(AustarModuleDescriptor.ModuleName) {
			usersModuleSchemaName = (new BetterCms.Module.Users.Models.Migrations.UsersVersionTableMetaData()).SchemaName;
		}

		public override void Up() {
			CreateHostRegistrationsTable();
		}

		private void CreateHostRegistrationsTable() {
			Create
				.Table("HostRegistrations")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("UserId").AsGuid().NotNullable()
				.WithColumn("GenderId").AsGuid().NotNullable()
				.WithColumn("BirthDate").AsDate().Nullable()
				.WithColumn("Occupation").AsString(MaxLength.Text).Nullable()
				.WithColumn("HomePhone").AsString(MaxLength.Name).NotNullable()
				.WithColumn("MobilePhone").AsString(MaxLength.Name).Nullable()
				.WithColumn("HomeAddress").AsString(MaxLength.Text).NotNullable()
				.WithColumn("HomeUnitNbr").AsString(50).Nullable()
				.WithColumn("HomeStreetNbr").AsString(50).Nullable()
				.WithColumn("HomeStreetName").AsString(MaxLength.Name).Nullable()
				.WithColumn("HomeCity").AsString(MaxLength.Name).Nullable()
				.WithColumn("HomePostCode").AsString(10).NotNullable()
				.WithColumn("HomeState").AsString(10).NotNullable();

			Create
				.ForeignKey("FK_HostRegistrations_User")
				.FromTable("HostRegistrations").InSchema(SchemaName).ForeignColumn("UserId")
				.ToTable("Users").InSchema(usersModuleSchemaName).PrimaryColumn("Id");
			Create
				.ForeignKey("FK_HostRegistrations_Gender")
				.FromTable("HostRegistrations").InSchema(SchemaName).ForeignColumn("GenderId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}
	}
}