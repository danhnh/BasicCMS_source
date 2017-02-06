using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611132200)]
	public class AlterHostRegistration3 : DefaultMigration {
		public AlterHostRegistration3()
			: base(AustarModuleDescriptor.ModuleName) {
		}

		public override void Up() {
			AlterHostRegistration();
			InsertDefaultData();
		}
		public override void Down() {
			throw new NotImplementedException();
		}
		private void AlterHostRegistration() {
			Alter
				.Table("HostRegistrations")
				.InSchema(SchemaName)
				.AddColumn("Family").AsString(MaxLength.Text).Nullable()
				.AddColumn("Home").AsString(MaxLength.Text).Nullable()
				.AddColumn("BankAccountName").AsString(MaxLength.Text).Nullable()
				.AddColumn("BankAccountNumber").AsString(MaxLength.Text).Nullable()
				.AddColumn("BankName").AsString(MaxLength.Text).Nullable()
				.AddColumn("BankBSB").AsString(MaxLength.Text).Nullable()
				.AddColumn("PLInsuranceProvider").AsString(MaxLength.Text).Nullable()
				.AddColumn("PLInsuranceNumber").AsString(MaxLength.Text).Nullable()
				.AddColumn("PLInsuranceExpiryDate").AsDate().Nullable()
				.AddColumn("HCInsuranceProvider").AsString(MaxLength.Text).Nullable()
				.AddColumn("HCInsuranceNumber").AsString(MaxLength.Text).Nullable()
				.AddColumn("HCInsuranceExpiryDate").AsDate().Nullable()
				.AddColumn("StudentPreferenceReason").AsString(MaxLength.Text).Nullable()
				.AddColumn("StudentOtherPreferences").AsString(MaxLength.Text).Nullable();
		}
		private void InsertDefaultData() {

			Update.Table("Classifications").InSchema(SchemaName).Set(new { Group = "HostPage2" }).AllRows();
			Update.Table("Classifications").InSchema(SchemaName).Set(new { Group = "" }).Where(new { SchemeCode = "GENDER" });
			Update.Table("Classifications").InSchema(SchemaName).Set(new { Group = "" }).Where(new { SchemeCode = "" });
			//H_FamilyMembers
			int i = 0;
			foreach (string s in new string[] { "1 member", "2 members", "3 members", "4 members", "5 members", "6 members", "7 members", "8 members", "9 members" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_FamilyMembers",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_MainReligion 
			i = 0;
			foreach (string s in new string[] { "Atheist","Buddhism", "Christianity", "Hinduism", "Islam", "Jewish" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_MainReligion",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Other", Description = "Other religion", SchemeCode = "H_MainReligion",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_HaveHosted
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = "H_HaveHosted",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "What experience do you have as a homestay family?", SchemeCode = "H_HaveHosted",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_HavePets
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = "H_HavePets",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please describe", Description = "What pets?", SchemeCode = "H_HavePets",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_PetsLiveInside
			i = 0;
			foreach (string s in new string[] { "Indoors", "Outdoors", "Indoors & Outdoors" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_PetsLiveInside",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_WhatPets
			i = 0;
			foreach (string s in new string[] { "Dog", "Cat", "Bird" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_WhatPets",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = true,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Other, please specify", Description = "Other pets", SchemeCode = "H_WhatPets",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = true,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//H_PublicLiabilityInsurance
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = "H_PublicLiabilityInsurance",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "", SchemeCode = "H_PublicLiabilityInsurance",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_HomeContentsInsurance
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = "H_HomeContentsInsurance",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "", SchemeCode = "H_HomeContentsInsurance",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Group = "HostPage3",
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//H_20MPublicInsurance
			i = 0;
			foreach (string s in new string[] { "No", "Yes" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_20MPublicInsurance",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage3",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentAgePreference
			i = 0;
			foreach (string s in new string[] { "Under 18", "Over 18", "No preference" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentAgePreference",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentGenderPreference
			i = 0;
			foreach (string s in new string[] { "Male", "Female", "No preference" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentGenderPreference",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentDietary,H_StudentAllergies
			foreach (var scheme in new string[] { "H_StudentDietary", "H_StudentAllergies" }) {
				i = 0;
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "No", Description = "No", SchemeCode = scheme,
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "", SchemeCode = scheme,
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentDietaryRequirements
			i = 0;
			foreach (string s in new string[] { "Vegetarian", "Gluten/Lactose Free", "No Pork", "Food Allergies" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentDietaryRequirements",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = true,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentAllergiesSupported
			i = 0;
			foreach (string s in new string[] { "Hay Fever", "Asthma", "Lactose Intolerance", "Gluten Intolerance", "Peanut Allergies", "Dust Allergies" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentAllergiesSupported",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = true,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Other", Description = "Details of students with allergies you can support?", SchemeCode = "H_StudentAllergiesSupported",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = true,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//H_StudentDisabilities
			i = 0;
			foreach (string s in new string[] { "No", "Yes" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentDisabilities",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_StudentSmoke
			i = 0;
			foreach (string s in new string[] { "No", "Yes (Outdoors)", "Yes (Indoors & Outdoors)" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_StudentSmoke",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//H_HearAbout
			i = 0;
			foreach (string s in new string[] { "Previous student", "Previous homestay family", "Website", "Facebook", "Google", "Agent" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s, SchemeCode = "H_HearAbout",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Other", Description = "Other referral please specify here", SchemeCode = "H_HearAbout",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
						Group = "HostPage4",
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
		}

	}
}