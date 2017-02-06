using System;
using BetterModules.Core.Models;

namespace bcms.austar.Models {
	[Serializable]
	public class MemberLanguage : EquatableEntity<MemberLanguage> {
		public virtual Member Member { get; set; }
		public virtual int LanguageIndex { get; set; }
		public virtual Classification Language { get; set; }
		public virtual Classification Proficiency { get; set; }
	}
}