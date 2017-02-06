using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611141000)]
	public class AlterStudentRegistration1 : DefaultMigration {
		public AlterStudentRegistration1() : base(AustarModuleDescriptor.ModuleName) {
			
		}
		public override void Up() {
			Alter
				.Table("StudentRegistrations")
				.InSchema(SchemaName)
				.AddColumn("PassportNumber").AsString(MaxLength.Text).Nullable()
				.AddColumn("PassportExpiryDate").AsDate().Nullable();

			Delete.FromTable("Classifications")
				.InSchema(SchemaName).Row(new { Group = "StudentPage3" });
			Delete.FromTable("Classifications")
				.InSchema(SchemaName).Row(new { SchemeCode = StudentSchemeCodes.S_AirportPickup });

			int i = 0;
			foreach (string s in new string[] { "Homestay Single Room", "Homestay Twin Share", "Homestay Self-Catered", "Homestay VIP Single Room", "Homestay VIP Self-Catered", "Student Shared Apartment", "Student Shared House" }) {
				Insert.IntoTable("Classifications")
					.InSchema(SchemaName)
					.Row(new {
						Id = Guid.NewGuid(), Name = s, Description = s,
						SchemeCode = StudentSchemeCodes.S_AccommodationType, Group = "StudentPage2",
						SortOrder = ++i, RequireMoreInfo = (i==2), MoreInfoDescription = "", IsMultiChoice = false,
						Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
					});
			}


			InsertOpts(StudentSchemeCodes.S_AirportPickup, "StudentPage2", "", false, false, "No", "Yes");

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
			for (int i = 0; i < opts.Length; i++)
				Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = opts[i], Description = infoDesc,
					SchemeCode = schemeCode, Group = group,
					SortOrder = (i + 1), RequireMoreInfo = lastNeedInfo && (i == opts.Length - 1), MoreInfoDescription = "", IsMultiChoice = isMulti,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
		}
	}
}