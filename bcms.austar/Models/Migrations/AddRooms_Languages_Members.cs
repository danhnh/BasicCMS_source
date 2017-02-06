using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201611161000)]
	public class AddRooms_Languages_Members : DefaultMigration {
		public AddRooms_Languages_Members() : base(AustarModuleDescriptor.ModuleName) { }

		public override void Up() {
			AddMemberTable();
			AddMemberLanguageTable();
			AddMemberInfoTable();
			AddStudentLanguageTable();
			AddRoomTable();
			AddRoomInfoTable();

			InsertClassificationData();
		}

		private void AddMemberTable() {
			Create
				.Table("Members")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("HostRegistrationId").AsGuid().NotNullable()
				.WithColumn("MemberIndex").AsInt32().NotNullable()
				.WithColumn("FirstName").AsString(MaxLength.Name).NotNullable()
				.WithColumn("LastName").AsString(MaxLength.Name).NotNullable()
				.WithColumn("BirthDate").AsDate().Nullable()
				.WithColumn("Occupation").AsString(MaxLength.Text).NotNullable()
				.WithColumn("HaveWWCC").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("ClearanceNumber").AsString(MaxLength.Name).NotNullable()
				.WithColumn("ClearanceExpiryDate").AsDate().Nullable()
				.WithColumn("WWCCScannedCopy").AsBinary().Nullable();

			Create
				.ForeignKey("FK_HostRegistration_Members")
				.FromTable("Members").InSchema(SchemaName).ForeignColumn("HostRegistrationId")
				.ToTable("HostRegistrations").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void AddMemberLanguageTable() {
			Create
				.Table("MemberLanguages")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("MemberId").AsGuid().NotNullable()
				.WithColumn("LanguageIndex").AsInt32().NotNullable()
				.WithColumn("LanguageId").AsGuid().NotNullable()
				.WithColumn("ProficiencyId").AsGuid().NotNullable();

			Create
				.ForeignKey("FK_Member_MemberLanguages")
				.FromTable("MemberLanguages").InSchema(SchemaName).ForeignColumn("MemberId")
				.ToTable("Members").InSchema(SchemaName).PrimaryColumn("Id");

			Create
				.ForeignKey("FK_MemberLanguage_Languages")
				.FromTable("MemberLanguages").InSchema(SchemaName).ForeignColumn("LanguageId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");

			Create
				.ForeignKey("FK_MemberLanguage_LanguageProficiency")
				.FromTable("MemberLanguages").InSchema(SchemaName).ForeignColumn("ProficiencyId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void AddMemberInfoTable() {
			Create
				.Table("MemberInfoes")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("MemberId").AsGuid().NotNullable()
				.WithColumn("ClassificationId").AsGuid().NotNullable()
				.WithColumn("SchemeCode").AsString(MaxLength.Name).NotNullable()
				.WithColumn("Description").AsString(MaxLength.Text).Nullable()
				.WithColumn("IsSelected").AsBoolean().NotNullable().WithDefaultValue(false);

			Create
				.ForeignKey("FK_MemberInfoes_Member")
				.FromTable("MemberInfoes").InSchema(SchemaName).ForeignColumn("MemberId")
				.ToTable("Members").InSchema(SchemaName).PrimaryColumn("Id");
			Create
				.ForeignKey("FK_MemberInfoes_Classification")
				.FromTable("MemberInfoes").InSchema(SchemaName).ForeignColumn("ClassificationId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void AddStudentLanguageTable() {
			Create
				.Table("StudentLanguages")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("StudentId").AsGuid().NotNullable()
				.WithColumn("LanguageIndex").AsInt32().NotNullable()
				.WithColumn("LanguageId").AsGuid().NotNullable()
				.WithColumn("ProficiencyId").AsGuid().NotNullable();

			Create
				.ForeignKey("FK_Student_StudentLanguages")
				.FromTable("StudentLanguages").InSchema(SchemaName).ForeignColumn("StudentId")
				.ToTable("StudentRegistrations").InSchema(SchemaName).PrimaryColumn("Id");

			Create
				.ForeignKey("FK_StudentLanguage_Languages")
				.FromTable("StudentLanguages").InSchema(SchemaName).ForeignColumn("LanguageId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");

			Create
				.ForeignKey("FK_StudentLanguage_LanguageProficiency")
				.FromTable("StudentLanguages").InSchema(SchemaName).ForeignColumn("ProficiencyId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void AddRoomTable() {
			Create
				.Table("Rooms")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("HostRegistrationId").AsGuid().NotNullable()
				.WithColumn("RoomType").AsInt32().NotNullable()
				.WithColumn("RoomIndex").AsInt32().NotNullable()
				.WithColumn("RoomNo").AsString(MaxLength.Name).NotNullable()
				.WithColumn("AvailableFrom").AsDate().Nullable()
				.WithColumn("AvailableTo").AsDate().Nullable()
				.WithColumn("DateLeaving").AsDate().Nullable();

			Create
				.ForeignKey("FK_HostRegistration_Rooms")
				.FromTable("Rooms").InSchema(SchemaName).ForeignColumn("HostRegistrationId")
				.ToTable("HostRegistrations").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void AddRoomInfoTable() {
			Create
				.Table("RoomInfoes")
				.InSchema(SchemaName)
				.WithBaseColumns()
				.WithColumn("RoomId").AsGuid().NotNullable()
				.WithColumn("ClassificationId").AsGuid().NotNullable()
				.WithColumn("SchemeCode").AsString(MaxLength.Name).NotNullable()
				.WithColumn("Description").AsString(MaxLength.Text).Nullable()
				.WithColumn("IsSelected").AsBoolean().NotNullable().WithDefaultValue(false);

			Create
				.ForeignKey("FK_RoomInfoes_Room")
				.FromTable("RoomInfoes").InSchema(SchemaName).ForeignColumn("RoomId")
				.ToTable("Rooms").InSchema(SchemaName).PrimaryColumn("Id");
			Create
				.ForeignKey("FK_RoomInfoes_Classification")
				.FromTable("RoomInfoes").InSchema(SchemaName).ForeignColumn("ClassificationId")
				.ToTable("Classifications").InSchema(SchemaName).PrimaryColumn("Id");
		}
		private void InsertClassificationData() {
			InsertOpts("L_Languages", "", "", false, false, "Albanian", "Arabic", "Balinese", "Bengali", "Burmese", "Chinese (Cantonese)", "Chinese (Mandarin)", "Dari", "Dutch", "English", "French", "German", "Greek", "Hazaragi", "Hindi", "Indonesian", "Italian", "Japanese",
					"Korean", "Kurdish (Kurmanji)", "Kurdish (Sorani)", "Macedonian", "Malaysian", "Pashto", "Persian", "Polish", "Portugese", "Russian", "Sinhalese", "Spanish", "Tagalog", "Tamil", "Thai", "Turkish", "Urdu", "Vietnamese");
			InsertOpts("L_LanguageProficiency", "", "", false, false, "Beginner", "Intermediate", "Advance", "Native");

			InsertOpts("R1_Type", "", "", false, false, "Single", "Twin", "Double");
			InsertOpts("R1_FlooringType", "", "Other flooring", true, false, "Carpet", "Timber/Wood", "Vinyl", "Tiles", "Other");
			InsertOpts("R1_IsDetached", "", "Describe the proposed detached accommodation", true, false, "No", "Yes, please describe");
			InsertOpts("R1_InternalEnsuite", "", "", false, false, "Yes", "No");
			InsertOpts("R1_AvailableImmediately", "", "", true, false, "Yes", "No, please specify the next available date");
			InsertOpts("R1_CurrentlyHosting", "", "", true, false, "No", "Yes, provide details below");
			InsertOpts("R1_StudentAge", "", "", false, false, "Over 18", "Under 18");
			InsertOpts("R1_StudentGender", "", "", false, false, "Female", "Male");
			InsertOpts("R1_StudentNationality", "", "", false, false, AddStudentRegistration.ListOfEthnicity);

			InsertOpts("R2_Available", "", "", false, false, "Yes", "No");
			InsertOpts("R2_Suite", "", "", false, true, "Toilet", "Shower", "Bath", "Ensuite");
			InsertOpts("R2_External", "", "", false, false, "Yes", "No");

			//Members
			InsertOpts("M_Title", "", "", false, false, "Mr.", "Mrs.", "Ms.", "Master", "Dr.");
			InsertOpts("M_Gender", "", "", false, false, "Male", "Female");
			InsertOpts("M_Role", "", "", false, false, "Host mother", "Host father", "Daughter", "Son", "Grandmother", "Grandfather", "Relatives", "Friends/Housemate", "Casual visitor (which overnights regularly)");
			InsertOpts("M_Ethnicity", "", "", false, false, AddStudentRegistration.ListOfEthnicity);
			InsertOpts("M_Smoker", "", "", false, false, "No", "Yes (Outdoors)", "Yes (Indoors & Outdoors)");
			InsertOpts("M_SpeakOther", "", "", false, false, "No", "Yes, please provide language details");
			InsertOpts("M_Languages", "", "", false, false, "1 language", "2 languages", "3 languages", "4 languages", "5 languages");
		}
		private void InsertOpts(string schemeCode, string group, string infoDesc, bool lastNeedInfo, bool isMulti, params string[] opts) {
			for (int i = 0; i < opts.Length; i++)
				Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = opts[i], Description = (lastNeedInfo && (i == opts.Length - 1))?infoDesc:"",
					SchemeCode = schemeCode, Group = group,
					SortOrder = (i + 1), RequireMoreInfo = lastNeedInfo && (i == opts.Length - 1), MoreInfoDescription = "", IsMultiChoice = isMulti,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
		}
	}
}
