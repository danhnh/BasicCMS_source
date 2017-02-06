using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCMS.Models
{
	public class MenuTranslationItem
	{
		public string Url { get; set; }
		public bool IsPublished { get; set; }
		public string LanguageCode { get; set; }
	}
}