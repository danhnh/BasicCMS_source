using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Services {
	public interface IClassificationService {
		List<Models.Classification> GetClassifications(string scheme);
		List<Models.Classification> GetClassificationsForRoom(string type);
		List<Models.Classification> GetClassificationsForMember();
		List<Models.Classification> GetClassificationsForLanguage();
		List<Models.Classification> GetClassificationsForHost(string group);
		List<Models.Classification> GetClassificationsForStudent(string group);
		List<Models.Classification> GetCommonClassifications(string group);
	}
}