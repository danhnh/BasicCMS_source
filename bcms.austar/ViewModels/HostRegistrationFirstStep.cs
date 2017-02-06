using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using BetterModules.Core.Models;
using bcms.austar.Content.Resources;
using BetterCms.Module.Root.Models;
using System.Security;

namespace bcms.austar.ViewModels {
	public class HostRegistrationFirstStep {
		public Guid Id { get; set; }
		
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "FirstName_Required")]
		[StringLength(MaxLength.Name, MinimumLength = 1, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "FirstName_Length")]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "LastName_Required")]
		[StringLength(MaxLength.Name, MinimumLength = 1, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "LastName_Length")]
		public string LastName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		[StringLength(MaxLength.Name, MinimumLength = 1, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "LengthExceeded")]
		[RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "InvalidEmail")]
		public string Email { get; set; }

		public IEnumerable<SelectListItem> GenderList {
			get {
				return new SelectListItem[] {
					new SelectListItem() { Value = "", Text = "Please select...", Selected = true },
					new SelectListItem() { Value = "D04ECC15-01F7-4940-B4AA-12F247C2CB1E", Text = "Male" },
					new SelectListItem() { Value = "51202FD8-5134-4B06-BB0F-DC5FF135AD39", Text = "Female" }
				};
			}
		}
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		public string GenderId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		public DateTime DateOfBirth { get; set; }
		
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		public string HomePhone { get; set; }
		public string MobilePhone { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		[StringLength(MaxLength.Name, MinimumLength = 6, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Password_Length")]
		public string Password { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		[StringLength(MaxLength.Text, MinimumLength = 1, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "LengthExceeded")]
		public string StreetAddress { get; set; }

		public string Address_UnitNo { get; set; }
		public string Address_StreetNo { get; set; }
		public string Address_Route { get; set; }
		public string Address_City { get; set; }
		public string Address_State { get; set; }
		public string Address_PostCode { get; set; }
		public bool PostalSameAsHome { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "Required")]
		[StringLength(MaxLength.Text, MinimumLength = 1, ErrorMessageResourceType = typeof(RegistrationGlobalization), ErrorMessageResourceName = "LengthExceeded")]
		public string PostalAddress { get; set; }
		public string Postal_UnitNo { get; set; }
		public string Postal_StreetNo { get; set; }
		public string Postal_Route { get; set; }
		public string Postal_City { get; set; }
		public string Postal_State { get; set; }
		public string Postal_PostCode { get; set; }
	}
}