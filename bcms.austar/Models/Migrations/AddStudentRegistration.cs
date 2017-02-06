using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611140000)]
	public class AddStudentRegistration : DefaultMigration {
		private readonly string usersModuleSchemaName;
		public AddStudentRegistration() : base(AustarModuleDescriptor.ModuleName) {
			usersModuleSchemaName = (new BetterCms.Module.Users.Models.Migrations.UsersVersionTableMetaData()).SchemaName;
		}

		public override void Up() {
			CreateStudentRegistrationsTable();
			InsertDefaultData();
		}

		private void CreateStudentRegistrationsTable() {
			Create
				.Table("StudentRegistrations")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("UserId").AsGuid().NotNullable()
				.WithColumn("GenderId").AsGuid().NotNullable()
				.WithColumn("BirthDate").AsDate().Nullable()
				.WithColumn("ArrivalDate").AsDate().Nullable()
				.WithColumn("TravelInsuranceExpiryDate").AsDate().Nullable()
				.WithColumn("TravelInsuranceScannedCopy").AsBinary().Nullable()
				.WithColumn("Yourself").AsString(MaxLength.Text).Nullable()
				.WithColumn("Family").AsString(MaxLength.Text).Nullable()
				.WithColumn("FavouriteFood").AsString(MaxLength.Text).Nullable()
				.WithColumn("TravelInsuranceProvider").AsString(MaxLength.Text).Nullable()
				.WithColumn("TravelInsuranceNumber").AsString(MaxLength.Text).Nullable()
				.WithColumn("ArrivalTime").AsString(MaxLength.Name).Nullable()
				.WithColumn("Airline").AsString(MaxLength.Name).Nullable()
				.WithColumn("FlightNumber").AsString(MaxLength.Name).Nullable()
				.WithColumn("HostPreferenceReason").AsString(MaxLength.Text).Nullable()
				.WithColumn("HostOtherPreferences").AsString(MaxLength.Text).Nullable()
				.WithColumn("College").AsString(MaxLength.Text).Nullable()
				.WithColumn("Campus").AsString(MaxLength.Text).Nullable()
				.WithColumn("CollegeAddress").AsString(MaxLength.Text).Nullable();

			Create
				.ForeignKey("FK_StudentRegistrations_User")
				.FromTable("StudentRegistrations").InSchema(SchemaName).ForeignColumn("UserId")
				.ToTable("Users").InSchema(usersModuleSchemaName).PrimaryColumn("Id");
			Create
				.ForeignKey("FK_StudentRegistrations_Gender")
				.FromTable("StudentRegistrations").InSchema(SchemaName).ForeignColumn("GenderId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}

		private void InsertDefaultData() {
			//S_HomeStayInThePast
			int i = 0;
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "No", Description = "",
						SchemeCode = StudentSchemeCodes.S_HomeStayInThePast, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "What experience do you have as a homestay student?",
						SchemeCode = StudentSchemeCodes.S_HomeStayInThePast, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//S_Ethnicity
			i = 0;
			foreach(string s in ListOfEthnicity) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_Ethnicity, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//S_Religion
			i = 0;
			foreach (string s in new string[] { "Atheist", "Buddhism", "Christianity", "Hinduism", "Islam", "Jewish" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_Religion, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Other", Description = "Other religion",
					SchemeCode = StudentSchemeCodes.S_Religion, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//S_Languages
			i = 0;
			foreach (string s in new string[] { "1 language", "2 languages", "3 languages", "4 languages", "5 languages" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_Languages, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//S_WithPets
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No",
					SchemeCode = StudentSchemeCodes.S_WithPets, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please describe", Description = "What pets?",
					SchemeCode = StudentSchemeCodes.S_WithPets, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//S_PetsLiveInside
			i = 0;
			foreach (string s in new string[] { "Indoors", "Outdoors", "Indoors & Outdoors" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_PetsLiveInside, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			//S_WhatPets
			i = 0;
			foreach (string s in new string[] { "Dog", "Cat", "Bird" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_WhatPets, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = true,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}
			Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = "Other, please specify", Description = "Other pets",
						SchemeCode = StudentSchemeCodes.S_WhatPets, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = true,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			//S_TravelInsurance
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No",
					SchemeCode = StudentSchemeCodes.S_TravelInsurance, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes, please provide details", Description = "",
					SchemeCode = StudentSchemeCodes.S_TravelInsurance, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//S_GuardianshipRequested
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Yes", Description = "Yes",
					SchemeCode = StudentSchemeCodes.S_GuardianshipRequested, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "No", Description = "No",
					SchemeCode = StudentSchemeCodes.S_GuardianshipRequested, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			//S_Guardianship
			i = 0;
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Austar", Description = "Austar",
					SchemeCode = StudentSchemeCodes.S_Guardianship, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = false, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
			Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = "Other", Description = "If other, please specify",
					SchemeCode = StudentSchemeCodes.S_Guardianship, Group = "StudentPage2",
					SortOrder = ++i, RequireMoreInfo = true, MoreInfoDescription = "", IsMultiChoice = false,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
		}


		public static string[] ListOfEthnicity = new string[] {
			"Afghan",
			"Albanian",
			"Algerian",
			"American",
			"Angolan",
			"Argentine",
			"Australian",
			"Austrian",
			"Bangladeshi",
			"Belarusian",
			"Belgian",
			"Bolivian",
			"Bosnian/Herzegovinian",
			"Brazilian",
			"British",
			"Bulgarian",
			"Cambodian",
			"Cameroonian",
			"Canadian",
			"Central African",
			"Chadian",
			"Chinese",
			"Colombian",
			"Congolese",
			"Congolese",
			"Costa Rican",
			"Croatian",
			"Czech",
			"Danish",
			"Dutch",
			"Ecuadorian",
			"Egyptian",
			"Emirati",
			"English",
			"Estonian",
			"Ethiopian",
			"Filipino",
			"Finnish",
			"French",
			"German",
			"Ghanaian",
			"Greek",
			"Guatemalan",
			"Honduran",
			"Hungarian",
			"Icelandic",
			"Indian",
			"Indonesian",
			"Iranian",
			"Iraqi",
			"Irish",
			"Israeli",
			"Italian",
			"Ivorian",
			"Jamaican",
			"Japanese",
			"Jordanian",
			"Kazakh",
			"Kenyan",
			"Korean",
			"Lao",
			"Latvian",
			"Libyan",
			"Lithuanian",
			"Malagasy",
			"Malaysian",
			"Malian",
			"Mauritanian",
			"Mexican",
			"Moroccan",
			"Namibian",
			"Nicaraguan",
			"Nigerian",
			"Norwegian",
			"Omani",
			"Other",
			"Pakistani",
			"Panamanian",
			"Paraguayan",
			"Peruvian",
			"Polish",
			"Portuguese",
			"Romanian",
			"Russian",
			"Salvadoran",
			"Saudi, Saudi Arabian",
			"Scottish",
			"Senegalese",
			"Serbian",
			"Singaporean",
			"Slovak",
			"Somalian",
			"South African",
			"Spanish",
			"Sudanese",
			"Swedish",
			"Swiss",
			"Syrian",
			"Taiwanese",
			"Thai",
			"Tunisian",
			"Turkish",
			"Turkmen",
			"Ukranian",
			"Uruguayan",
			"Vietnamese",
			"Welsh",
			"Zambian",
			"Zimbabwean" };
	}
}