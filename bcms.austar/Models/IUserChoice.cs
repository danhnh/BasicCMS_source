using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Models {
	public interface IUserChoice {
		Classification Classification { get; set; }
		string SchemeCode { get; set; }
		string Description { get; set; }
		bool IsSelected { get; set; }
	}
}