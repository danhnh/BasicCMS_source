using BetterCms.Core.Exceptions.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcms.austar.Exceptions {
	public class UniqueUniversityException : ValidationException {
		public UniqueUniversityException(Func<string> resource, string message)
            : base(resource, message)
        {
		}

		public UniqueUniversityException(Func<string> resource, string message, Exception innerException)
            : base(resource, message, innerException)
        {
		}
	}
}