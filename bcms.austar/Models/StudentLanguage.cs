using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class StudentLanguage : EquatableEntity<StudentLanguage> {
		public virtual StudentRegistration Student { get; set; }
		public virtual int LanguageIndex { get; set; }
		public virtual Classification Language { get; set; }
		public virtual Classification Proficiency { get; set; }
	}
}