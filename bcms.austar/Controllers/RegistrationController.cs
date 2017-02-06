
using bcms.austar.Commands;
using bcms.austar.Services;
using bcms.austar.ViewModels;
using BetterCms.Core.Security;
using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using Microsoft.Web.Mvc;
using System.Web.Mvc;
using BetterCms.Module.Users.Services;
using bcms.austar.Content.Resources;
using BetterModules.Core.Web.Models;
using BetterCms.Module.Users.ViewModels.User;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using BetterCms.Module.Users.Commands.Authentication;
using BetterCms.Module.Users.ViewModels.Authentication;
using BetterModules.Core.DataAccess.DataContext;
using BetterModules.Core.DataAccess;

namespace bcms.austar.Controllers
{
	[ActionLinkArea(AustarModuleDescriptor.ModuleAreaName)]
	public class RegistrationController : CmsControllerBase {
		private readonly IUserService userService;
		private readonly IRoleService roleService;
		private readonly IRegistrationService registrationService;
		private readonly IClassificationService classificationService;
		public RegistrationController(IUserService us, IRoleService rs, IRegistrationService reg, IClassificationService classificationService) {
			this.userService = us;
			this.roleService = rs;
			this.registrationService = reg;
			this.classificationService = classificationService;
		}
		[HttpPost]
		public JsonResult CheckEmail(string id) {
			int count = 0;
			var users = userService.FindUsersByEmail(id, 0, 1, out count);
			if (count > 0)
				return Json(new { @id = id, @valid = false }, JsonRequestBehavior.DenyGet);
			return Json(new { @id = id, @valid = true }, JsonRequestBehavior.DenyGet);
		}
		public ActionResult Reg() {
			if (User.Identity.IsAuthenticated) {
				if (User.IsInRole("AustarStudent")) return CreateStudent("");
				else if (User.IsInRole("AustarHost")) return Create("");
			}
			return Redirect("/Login");
		}
		public ActionResult CreateStudent(string step) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetStudentRegistration(User.Identity.Name);
				if (register != null) {
					var choices = registrationService.GetHostChoices(User.Identity.Name, string.Empty);
					if (choices.Count == 0) {
						var user = userService.GetUser(User.Identity.Name);
						if (user != null) {
							registrationService.AddStudentChoiceClassifications(user);
							choices = registrationService.GetHostChoices(User.Identity.Name, string.Empty);
						}
					}
					
					var model1 = new StudentRegistrationViewModel(register) {
						Id = register.Id,
						Choices = choices,
						Languages = registrationService.GetStudentLanguages(User.Identity.Name),
						Classifications = classificationService.GetClassificationsForStudent("")
									.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
					};
					ViewData.Add("Choices", model1.Choices);

					if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "StudentPage2") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("studentpage2"))) {
						return View("StudentStep2", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "StudentPage3") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("studentpage3"))) {
						//var model1 = new StudentRegistrationStep3() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "StudentPage3").ToList(),
						//	Classifications = classificationService.GetClassificationsForStudent("StudentPage3")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};
						return View("StudentStep3", model1);
					}
				}
				return View("ThanksStudent");
			}
			var model = new bcms.austar.ViewModels.StudentRegistrationFirstStep() { Id = Guid.Empty };
			return View("StudentFirstStep", model);
		}
		public ActionResult Create(string step) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if( register != null) {
					var choices = registrationService.GetHostChoices(User.Identity.Name, string.Empty);
					if( choices.Count == 0 ) {
						var user = userService.GetUser(User.Identity.Name);
						if (user != null) {
							registrationService.AddHostChoiceClassifications(user);
							choices = registrationService.GetHostChoices(User.Identity.Name, string.Empty);
						}
					}
					var rooms = registrationService.GetHostRooms(User.Identity.Name);
					if (rooms.Count < 1) {
						registrationService.AddRooms(register, 1);
						registrationService.AddRooms(register, 2);
						rooms = registrationService.GetHostRooms(User.Identity.Name);
					}
					var members = registrationService.GetHostMembers(User.Identity.Name);
					if (members.Count < 1) {
						registrationService.AddMembers(register);
						members = registrationService.GetHostMembers(User.Identity.Name);
					}
					var model1 = new HostRegistrationViewModel(register) {
						Id = register.Id,
						Choices = choices,
						Rooms = rooms,
						Members = members,
						MemberLanguages = registrationService.GetHostMemberLanguages(User.Identity.Name),
						Classifications = classificationService.GetClassificationsForHost(string.Empty)
									.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
					};
					ViewData.Add("Choices", model1.Choices);
					ViewData.Add("Rooms", model1.Rooms);
					ViewData.Add("Members", model1.Members);
					if ( (string.IsNullOrEmpty(step) &&
						choices.Count(x=>x.IsSelected && x.Classification.Group == "HostPage2") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage2"))) {
						//var model1 = new HostRegistrationStep2() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage2").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage2")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};
						return View("Step2", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "HostPage3") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage3"))) {
						//var model1 = new HostRegistrationStep3() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage3").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage3")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};
						return View("Step3", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "HostPage4") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage4"))) {
						//var model1 = new HostRegistrationStep4() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage4").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage4")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};
						return View("Step4", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "HostPage5") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage5"))) {
						//var model1 = new HostRegistrationStep5() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage5").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage5")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};
						
						return View("Step5", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "HostPage6") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage6"))) {
						//var model1 = new HostRegistrationStep3() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage6").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage6")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};

						return View("Step6", model1);
					} else if ((string.IsNullOrEmpty(step) && choices.Count(x => x.IsSelected && x.Classification.Group == "HostPage7") == 0) || (!string.IsNullOrEmpty(step) && step.ToLower().Equals("hostpage7"))) {
						//var model1 = new HostRegistrationStep7() {
						//	Id = register.Id,
						//	Choices = choices.Where(x => x.Classification.Group == "HostPage7").ToList(),
						//	Classifications = classificationService.GetClassificationsForHost("HostPage7")
						//			.Union(classificationService.GetCommonClassifications(string.Empty)).ToList()
						//};

						return View("Step7", model1);
					}

					return View("ThanksHost");
				}
			}
			var model = new bcms.austar.ViewModels.HostRegistrationFirstStep() { Id = Guid.Empty, PostalSameAsHome = true };
			return View("Edit", model);
		}
		public ActionResult CreateStudentUser(StudentRegistrationFirstStep vm) {
			if (ModelState.IsValid) {
				if (User.Identity.IsAuthenticated) {
					Messages.AddWarn(RegistrationGlobalization.AlreadyRegister);
					return WireJson(false);
				}
				int count = 0;
				var users = userService.FindUsersByEmail(vm.Email, 0, 1, out count);
				if (count > 0) {
					Messages.AddError(RegistrationGlobalization.Email_Exists);
					return WireJson(false);
				}
				EditUserViewModel userVM = new EditUserViewModel() {
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					Email = vm.Email,
					Version = 0,
					Id = Guid.Empty,
					UserName = vm.Email,
					Password = vm.Password,
					RetypedPassword = vm.Password,
					Roles = new List<string>(new string[] { "AustarStudent" })
				};
				var user = userService.SaveUser(userVM);
				var response = GetCommand<BetterCms.Module.Users.Commands.User.SaveUser.SaveUserCommand>().ExecuteCommand(userVM);

				HttpCookie authCookie = GetCommand<LoginCommand>().ExecuteCommand(new LoginViewModel() { UserName = vm.Email, Password = vm.Password, RememberMe = true, IsFormsAuthenticationEnabled = FormsAuthentication.IsEnabled });
				if (authCookie != null) {
					Response.Cookies.Add(authCookie);
				}


				vm.Id = user.Id;
				vm.Password = "";

				var gRegistrationId = registrationService.StudentRegister(vm);
				return WireJson(true, data: vm);
			}
			return WireJson(false);
		}
		public ActionResult CreateUser(bcms.austar.ViewModels.HostRegistrationFirstStep vm) {
			if (ModelState.IsValid) {
				if (User.Identity.IsAuthenticated) {
					Messages.AddWarn(RegistrationGlobalization.AlreadyRegister);
					return WireJson(false);
				}
				int count = 0;
				var users = userService.FindUsersByEmail(vm.Email, 0, 1, out count);
				if (count > 0) {
					Messages.AddError(RegistrationGlobalization.Email_Exists);
					return WireJson(false);
				}
				EditUserViewModel userVM = new EditUserViewModel() {
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					Email = vm.Email,
					Version = 0,
					Id = Guid.Empty,
					UserName = vm.Email,
					Password = vm.Password,
					RetypedPassword = vm.Password,
					Roles = new List<string>(new string[] { "AustarHost" })
				};
				var user = userService.SaveUser(userVM);
				var response = GetCommand<BetterCms.Module.Users.Commands.User.SaveUser.SaveUserCommand>().ExecuteCommand(userVM);

				HttpCookie authCookie = GetCommand<LoginCommand>().ExecuteCommand(new LoginViewModel() { UserName = vm.Email, Password = vm.Password, RememberMe = true, IsFormsAuthenticationEnabled = FormsAuthentication.IsEnabled });
				if (authCookie != null) {
					Response.Cookies.Add(authCookie);
				}


				vm.Id = user.Id;
				vm.Password = "";

				var gRegistrationId = registrationService.HostRegister(vm);
				return WireJson(true, data: vm);
			}
			return WireJson(false);
		}
		public ActionResult DescribeStudent(StudentRegistrationViewModel vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetStudentRegistration(User.Identity.Name);
				if (register != null) {
					register.Yourself = vm.Yourself;
					register.Family = vm.Family;
					register.FavouriteFood = vm.FavouriteFood;
					register.TravelInsuranceProvider = vm.TravelInsuranceProvider;
					register.TravelInsuranceNumber = vm.TravelInsuranceNumber;
					register.TravelInsuranceExpiryDate = vm.TravelInsuranceExpiryDate;
					register.ArrivalDate = vm.ArrivalDate;
					register.ArrivalTime = vm.ArrivalTime;
					register.Airline = vm.Airline;
					register.FlightNumber = vm.FlightNumber;
					this.registrationService.UpdateStudentRegistration(register);
					var updateResult = this.registrationService.UpdateStudentChoices(User.Identity.Name, vm.Id, "StudentPage2", Request.Params);
					return WireJson(updateResult);
				}
			}
			return WireJson(false);
		}
		public ActionResult DescribeStudentMore(StudentRegistrationStep3 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetStudentRegistration(User.Identity.Name);
				if (register != null) {
					register.HostPreferenceReason = vm.HostPreferenceReason;
					register.HostOtherPreferences = vm.HostOtherPreferences;
					register.College = vm.College;
					register.Campus = vm.Campus;
					register.CollegeAddress = vm.CollegeAddress;
					this.registrationService.UpdateStudentRegistration(register);
					var updateResult = this.registrationService.UpdateStudentChoices(User.Identity.Name, vm.Id, "StudentPage3", Request.Params);
					return WireJson(updateResult);
				}
			}
			return WireJson(false);
		}
		public ActionResult DescribeHost(HostRegistrationStep2 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if (register != null) { 
					register.PLInsuranceProvider = vm.PLInsuranceProvider;
					register.PLInsuranceNumber = vm.PLInsuranceNumber;
					register.PLInsuranceExpiryDate = vm.PLInsuranceExpiryDate;
					register.HCInsuranceProvider = vm.HCInsuranceProvider;
					register.HCInsuranceNumber = vm.HCInsuranceNumber;
					register.HCInsuranceExpiryDate = vm.HCInsuranceExpiryDate;
					register.HomeComments = vm.HomeComments;
					this.registrationService.UpdateHostRegistration(register);
				}
				var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage2", Request.Params);
				return WireJson(updateResult);
			}
			return WireJson(false);
		}
		public ActionResult DescribeHost5(HostRegistrationStep5 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if (register != null) {
					register.ExtraEmails = vm.ExtraEmails;
					register.GuestWellcomeMessage = vm.GuestWellcomeMessage;
					register.PublicProfile = vm.PublicProfile;
					register.AdditionalCosts = vm.AdditionalCosts;
					register.HouseholdOccupations = vm.HouseholdOccupations;
					register.PublicComments = vm.PublicComments;
					this.registrationService.UpdateHostRegistration(register);
				}
				var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage5", Request.Params);
				return WireJson(updateResult);
			}
			return WireJson(false);
		}
		public ActionResult DescribeHost6(HostRegistrationStep3 vm) {
			if (User.Identity.IsAuthenticated) {
				var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage6", Request.Params)
					&& this.registrationService.UpdateHostMembers(User.Identity.Name, vm.Id, Request.Params);
				return WireJson(updateResult);
			}
			return WireJson(false);
		}
		public ActionResult DescribeHost7(HostRegistrationStep7 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if (register != null) {
					register.BankAccountName = vm.BankAccountName;
					register.BankAccountNumber = vm.BankAccountNumber;
					register.BankBSB = vm.BankBSB;
					register.BankName = vm.BankName;

					this.registrationService.UpdateHostRegistration(register);
				}
					var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage7", Request.Params);
				return WireJson(updateResult);
			}
			return WireJson(false);
		}
		public ActionResult DescribeStudentPreference(HostRegistrationStep4 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if (register != null) {
					register.StudentPreferenceReason = vm.StudentPreferenceReason;
					register.StudentOtherPreferences = vm.StudentOtherPreferences;
					register.WillHostNationality = vm.WillHostNationality;
					register.ServiceComments = vm.ServiceComments;
					this.registrationService.UpdateHostRegistration(register);
					var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage4", Request.Params)
									&& this.registrationService.UpdateHostRooms(User.Identity.Name, vm.Id, Request.Params);
					return WireJson(updateResult);
				}
				
			}
			return WireJson(false);
		}
		public ActionResult DescribeHostFamily(HostRegistrationStep3 vm) {
			if (User.Identity.IsAuthenticated) {
				var register = this.registrationService.GetHostRegistration(User.Identity.Name);
				if( register != null) {
					register.Family = vm.Family;
					register.Home = vm.Home;
					register.OtherAboutPets = vm.OtherAboutPets;
					register.EmergencyContact = vm.EmergencyContact;
					register.LifestyleComments = vm.LifestyleComments;

					this.registrationService.UpdateHostRegistration(register);
					var updateResult = this.registrationService.UpdateHostChoices(User.Identity.Name, vm.Id, "HostPage3", Request.Params);
					return WireJson(updateResult);
				}				
			}
			return WireJson(false);
		}
	}
}