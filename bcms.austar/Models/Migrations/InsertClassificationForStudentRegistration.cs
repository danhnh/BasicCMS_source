using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611140900)]
	public class InsertClassificationForStudentRegistration : DefaultMigration {
		
		public InsertClassificationForStudentRegistration() : base(AustarModuleDescriptor.ModuleName) {
		
		}

		public override void Up() {
			InsertDefaultData();
		}
		
		private void InsertDefaultData() {
			int i = 0;
			//S_HomeStayInThePast inserted in the last migration

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


			InsertOpts(StudentSchemeCodes.S_AirportPickup, "StudentPage2", "",false, false, "No", "Yes");

			InsertOpts(StudentSchemeCodes.S_WithChildrenU10, "StudentPage3", "", false, false, "Yes", "No");
			InsertOpts(StudentSchemeCodes.S_WithChildrenU17, "StudentPage3", "", false, false, "Yes", "No");
			InsertOpts(StudentSchemeCodes.S_WithSmoker, "StudentPage3", "", false, false, "No", "Yes (Outdoors)", "Yes (Indoors & Outdoors)");
			InsertOpts(StudentSchemeCodes.S_Dietary, "StudentPage3", "", true, false, "No", "Yes, please provide details");
			InsertOpts(StudentSchemeCodes.S_DietaryRequirements, "StudentPage3", "Other dietary requirements", true, true, "Vegetarian", "Gluten/Lactose Free", "No Pork", "Food Allergies", "Other, please provide details");
			InsertOpts(StudentSchemeCodes.S_Allergies, "StudentPage3", "", true, false, "No", "Yes, please provide details");
			InsertOpts(StudentSchemeCodes.S_TypeOfAllergies, "StudentPage3", "Detail of other allergies", true, true, "Hay Fever", "Asthma", "Lactose Intolerance", "Gluten Intolerance", "Peanut Allergies", "Dust Allergies", "Other, please provide details");


			InsertOpts(StudentSchemeCodes.S_Medication, "StudentPage3", "Details of any medication you take?", true, false, "No", "Yes, please provide details");
			InsertOpts(StudentSchemeCodes.S_Disabilities, "StudentPage3", "Please provide details of any disabilities", true, false, "No", "Yes, please provide details");

			InsertOpts(StudentSchemeCodes.S_Smoke, "StudentPage3", "", false, false, "No", "Yes (Outdoors)", "Yes (Indoors & Outdoors)");

			InsertOpts(StudentSchemeCodes.S_HearAbout, "StudentPage3", "Other referral please specify here", true, false, "Previous student", "Previous homestay family", "Website", "Facebook", "Google", "Agent", "Other");
			InsertOpts(StudentSchemeCodes.S_ReasonForChoosing, "StudentPage3", "Other reason for choosing homestay", true, false, "Study", "Internship", "Other, please specify");
		}


		private void InsertOpts(string schemeCode, string group, string infoDesc, bool lastNeedInfo, bool isMulti, params string[] opts) {
			for(int i = 0; i<opts.Length; i++)
				Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = opts[i], Description = opts[i],
					SchemeCode = schemeCode, Group = group,
					SortOrder = ++i, RequireMoreInfo = lastNeedInfo&&(i==opts.Length-1), MoreInfoDescription = "", IsMultiChoice = isMulti,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
		}
		string[] ListOfEthnicity = new string[] {
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