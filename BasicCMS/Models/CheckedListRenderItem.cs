using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCMS.Models {
	public class CheckedListRenderItem {
		public int Group { get; set; }
		public IEnumerable<bcms.austar.Models.Classification> Classifications { get; set; }
		public CheckedListRenderItem() {
			Group = -1;
		}
	}
}