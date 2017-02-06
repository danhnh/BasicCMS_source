using System;
using BetterCms.Module.Root.Mvc.Grids;
using System.ComponentModel.DataAnnotations;
using BetterCms.Module.Root.Content.Resources;
using BetterModules.Core.Models;

namespace bcms.austar.ViewModels {
	public class UniversityViewModel : IEditableGridItem {
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual Guid Id { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual int Version { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual string Name { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual string ShortName { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual string StreetAddress { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		[StringLength(10, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
		public virtual string PostCode { get; set; }
		[Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
		public virtual string WebsiteUrl { get; set; }
	}
}