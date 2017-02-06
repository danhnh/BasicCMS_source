using System;

using BetterModules.Core.DataAccess.DataContext.Migrations;
using BetterModules.Core.Models;

using FluentMigrator;

namespace bcms.austar.Models.Migrations {
	[Migration(201612312300)]
	public class AddMoreHostOptions : DefaultMigration {
		public AddMoreHostOptions() : base(AustarModuleDescriptor.ModuleName) {
		}
		public override void Up() {
			InsertOpts("H_AnySmoker", "HostPage3", "", false, false, "No", "Yes");
			InsertOpts("H_Ethnicity", "HostPage3", "", false, false, AddStudentRegistration.ListOfEthnicity);
			InsertOpts("H_OtherEthnicity", "HostPage3", "", false, false, AddStudentRegistration.ListOfEthnicity);
			InsertOpts("H_Languages", "HostPage3", "", false, false, "Albanian", "Arabic", "Balinese", "Bengali", "Burmese", "Chinese (Cantonese)", "Chinese (Mandarin)", "Dari", "Dutch", "English", "French", "German", "Greek", "Hazaragi", "Hindi", "Indonesian", "Italian", "Japanese",
					"Korean", "Kurdish (Kurmanji)", "Kurdish (Sorani)", "Macedonian", "Malaysian", "Pashto", "Persian", "Polish", "Portugese", "Russian", "Sinhalese", "Spanish", "Tagalog", "Tamil", "Thai", "Turkish", "Urdu", "Vietnamese");
			InsertOpts("H_OtherLanguages", "HostPage3", "", false, false, "Albanian", "Arabic", "Balinese", "Bengali", "Burmese", "Chinese (Cantonese)", "Chinese (Mandarin)", "Dari", "Dutch", "English", "French", "German", "Greek", "Hazaragi", "Hindi", "Indonesian", "Italian", "Japanese",
					"Korean", "Kurdish (Kurmanji)", "Kurdish (Sorani)", "Macedonian", "Malaysian", "Pashto", "Persian", "Polish", "Portugese", "Russian", "Sinhalese", "Spanish", "Tagalog", "Tamil", "Thai", "Turkish", "Urdu", "Vietnamese");
		}

		private void InsertOpts(string schemeCode, string group, string infoDesc, bool lastNeedInfo, bool isMulti, params string[] opts) {
			for (int i = 0; i < opts.Length; i++)
				Insert.IntoTable("Classifications")
				.InSchema(SchemaName)
				.Row(new {
					Id = Guid.NewGuid(), Name = opts[i], Description = (lastNeedInfo && (i == opts.Length - 1)) ? infoDesc : "",
					SchemeCode = schemeCode, Group = group,
					SortOrder = (i + 1), RequireMoreInfo = lastNeedInfo && (i == opts.Length - 1), MoreInfoDescription = "", IsMultiChoice = isMulti,
					Version = "1", CreatedByUser = "Admin CMS", ModifiedByUser = "Admin CMS"
				});
		}
	}
}