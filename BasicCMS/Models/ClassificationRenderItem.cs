using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCMS.Models {
	public class ClassificationRenderItem {
		public string SchemeCode { get; set; }
		public string Title { get; set; }
		public IEnumerable<bcms.austar.Models.Classification> Classifications { get; set; }
		public int Column { get; set; }
		public int GroupIndex { get; set; }
		public int GroupIndex2 { get; set; }
		public bool IsRequired { get; set; }
		public bool RenderMoreInfo { get; set; }
		public bool ShowDetailsByIndex { get; set; }
		public int ColumnForMoreInfo { get; set; }
		public bcms.austar.Models.Classification SelectedValue { get; set; }
		public bcms.austar.ViewModels.IRegistrationViewModel viewModel { get; set; }
		public ClassificationRenderItem() {
			GroupIndex = GroupIndex2 = -1;
			Column = 4;
			RenderMoreInfo = true;
			ColumnForMoreInfo = 4;
			ShowDetailsByIndex = false;
		}
		public ClassificationRenderItem(bcms.austar.ViewModels.IRegistrationViewModel VM)
			: this() {
			viewModel = VM;
		}

		public string GetClassificationInfoValue(string schemeCode, int index = -1) {
			if (viewModel != null) return viewModel.GetClassificationInfoValue(schemeCode, index);
			return "";
		}
	}
}