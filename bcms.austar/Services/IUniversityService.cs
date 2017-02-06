using bcms.austar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Services {
	public interface IUniversityService {
		University SaveUniversity(string name, string shortname, System.Guid id, int version);
		University SaveUniversity(string name, string shortname);
	}
}