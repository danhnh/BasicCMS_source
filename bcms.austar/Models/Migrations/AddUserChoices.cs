using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611111400)]
	public class AddUserChoices : DefaultMigration {
		private readonly string usersModuleSchemaName;
		public AddUserChoices()
			: base(AustarModuleDescriptor.ModuleName) {
			usersModuleSchemaName = (new BetterCms.Module.Users.Models.Migrations.UsersVersionTableMetaData()).SchemaName;
		}

		public override void Up() {
			AlterClassificationsTable();
			CreateUserChoicesTable();
			InsertDefaultData();
		}
		public override void Down() {
			throw new NotImplementedException();
		}
		private void AlterClassificationsTable() {
			Alter
				.Table("Classifications")
				.InSchema(SchemaName)
				.AddColumn("IsMultiChoice").AsBoolean().NotNullable().WithDefaultValue(false);
		}
		private void CreateUserChoicesTable() {
			Create
				.Table("UserChoices")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("UserId").AsGuid().NotNullable()
				.WithColumn("ClassificationId").AsGuid().NotNullable()
				.WithColumn("SchemeCode").AsString(MaxLength.Name).NotNullable()
				.WithColumn("Description").AsString(MaxLength.Text).Nullable()
				.WithColumn("IsSelected").AsBoolean().NotNullable().WithDefaultValue(false);

			Create
				.ForeignKey("FK_UserChoices_User")
				.FromTable("UserChoices").InSchema(SchemaName).ForeignColumn("UserId")
				.ToTable("Users").InSchema(usersModuleSchemaName).PrimaryColumn("Id");
			Create
				.ForeignKey("FK_UserChoices_Classification")
				.FromTable("UserChoices").InSchema(SchemaName).ForeignColumn("ClassificationId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}


		private void InsertDefaultData() {
			//H_DwellingType
			int i = 0;
			foreach (string s in new string[] { "House", "Townhouse", "Duplex/Semi", "Apartment", "Self Contained (e.g. Granny Flat)" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_DwellingType",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_MainFlooringType
			i = 0;
			foreach (string s in new string[] { "Carpet", "Timber/Wood", "Vinyl", "Tiles" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_MainFlooringType",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}

			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Other", Description = "Other type of flooring", SchemeCode = "H_MainFlooringType",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_InternetAvailable
			i = 0;
			foreach (string s in new string[] { "No", "Yes ($10/week will be payable directly to you by the student)" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_InternetAvailable",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_SmokingDetector
			i = 0;
			foreach (string s in new string[] { "No", "Yes" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_SmokingDetector",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_Facilities
			i = 0;
			foreach (string s in new string[] { "Swimming Pool", "Tennis Court", "Piano", "Gym", "Disabled Access" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_Facilities",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = true,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Other (please describe)", Description = "Other facilities", SchemeCode = "H_Facilities",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = true,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//H_NearestBusStop,H_NearestTrainStop,H_NearestFerryStop
			foreach (string d in new string[] { "H_NearestBusStop", "H_NearestTrainStop", "H_NearestFerryStop" }) {
				i = 0;
				foreach (string s in new string[] { "0m - 100m", "101m - 200m", "201m - 1km", "> 1km", "Not applicable" }) {
					Insert.IntoTable("Classifications")
						.InSchema(SchemaName)
						.Row(new {
							Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = d,
							SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
							Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
						});
				}
			}
			//H_NumberOfBedrooms
			i = 0;
			for(i=1; i<=6; i++) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = i.ToString(), Description = i.ToString(), SchemeCode = "H_NumberOfBedrooms",
						SortOrder = i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_AvailableBedrooms			
			i = 0;
			foreach (string s in new string[] { "1 Bedroom", "2 Bedrooms", "3 Bedrooms", "4 Bedrooms", "5 Bedrooms" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_AvailableBedrooms",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_Bathrooms
			i = 0;
			foreach (string s in new string[] { "1 Bathroom", "2 Bathrooms", "3 Bathrooms", "4 Bathrooms", "5 Bathrooms" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_Bathrooms",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_LaundryFacilities
			i = 0;
			//foreach (string s in new string[] { "No", "Yes" }) {
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = "H_LaundryFacilities",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Yes", Description = "Yes", SchemeCode = "H_LaundryFacilities",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//}
			//H_LaundryDay
			i = 0;
			foreach (string s in new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_LaundryDay",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_LaundryIsOutside
			i = 0;
			foreach (string s in new string[] { "No", "Yes" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_LaundryIsOutside",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
		}
	}
}