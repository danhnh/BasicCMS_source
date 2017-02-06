using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bcms.austar.ViewModels;
using BetterModules.Core.DataAccess;
using BetterModules.Core.DataAccess.DataContext;
using Common.Logging;
using bcms.austar.Models;
using BetterCms.Module.Root.Mvc;
using System.Collections.Specialized;

namespace bcms.austar.Services {
	public class DefaultRegistrationService : IRegistrationService {
		private IRepository repository;
		private IUnitOfWork unitOfWork;
		private IClassificationService classification;
		private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
		public DefaultRegistrationService(IRepository repository, IUnitOfWork unitOfWork, IClassificationService classification) {
			this.repository = repository;
			this.unitOfWork = unitOfWork;
			this.classification = classification;
		}
		public HostRegistration GetHostRegistration(string email) {
			return repository.FirstOrDefault<HostRegistration>(x => x.User.Email == email);
		}
		public List<RoomInfo> GetHostRooms(string email) {
			return repository
				.AsQueryable<RoomInfo>(x => x.Room.HostRegistration.User.Email == email)
				.ToList();
		}
		public List<MemberInfo> GetHostMembers(string email) {
			return repository
				.AsQueryable<MemberInfo>(x => x.Member.HostRegistration.User.Email == email)
				.ToList();
		}
		public List<StudentLanguage> GetStudentLanguages(string email) {
			return repository
				.AsQueryable<StudentLanguage>(x => x.Student.User.Email == email)
				.ToList();
		}
		public List<MemberLanguage> GetHostMemberLanguages(string email) {
			return repository
				.AsQueryable<MemberLanguage>(x => x.Member.HostRegistration.User.Email == email)
				.ToList();
		}
		public StudentRegistration GetStudentRegistration(string email) {
			return repository.FirstOrDefault<StudentRegistration>(x => x.User.Email == email);
		}
		public HostRegistration UpdateHostRegistration(HostRegistration reg) {
			repository.Save(reg);
			unitOfWork.Commit();
			return reg;
		}
		public StudentRegistration UpdateStudentRegistration(StudentRegistration reg) {
			repository.Save(reg);
			unitOfWork.Commit();
			return reg;
		}
		public List<UserChoice> GetHostChoices(string email, string group) {
			if(string.IsNullOrEmpty(group))
				return repository.AsQueryable<UserChoice>(x => x.User.Email == email).ToList();
			return repository.AsQueryable<UserChoice>(x => x.User.Email == email).Where(x => x.Classification.Group == group).ToList();
		}
		private void UpdateOneChoice(int index, IUserChoice uc, NameValueCollection param) {
			var code = string.Format("{0}[{1}]", uc.SchemeCode, index);
			var info = string.Format("{0}[{2}]_info[{1}]", uc.SchemeCode, uc.Classification.Id, index);
			if (!uc.Classification.IsMultiChoice) {
				Guid selected = Guid.Empty;
				if (Guid.TryParse(param[code], out selected)) {
					uc.IsSelected = selected.Equals(uc.Classification.Id);
				} else
					uc.IsSelected = false;
				uc.Description = "";
				if (uc.IsSelected && uc.Classification.RequireMoreInfo) {
					if (!string.IsNullOrEmpty(param[info]))
						uc.Description = param[code + "_info"];
					else if (!string.IsNullOrEmpty(param[code + "_info"]))
						uc.Description = param[code + "_info"];
				}
			} else {
				code = string.Format("{0}[{2}]_checked[{1}]", uc.SchemeCode, uc.Classification.Id, index);
				bool selected = false;
				if (!string.IsNullOrEmpty(param[code])) {
					string[] sel = param[code].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
					if (sel.Length > 0)
						bool.TryParse(sel[0], out selected);
				}
				uc.IsSelected = selected;
				uc.Description = "";
				if (selected && uc.Classification.RequireMoreInfo) {
					uc.Description = param[info] ?? "";
				}
			}
		}
		private bool UpdateRoomInfo(int index, List<RoomInfo> choices, NameValueCollection param) {
			foreach (var uc in choices) {
				UpdateOneChoice(index, uc, param);
				repository.Save(uc);
			}
			return true;
		}
		private bool UpdateMemberInfo(int index, List<MemberInfo> choices, NameValueCollection param) {
			foreach (var uc in choices) {
				UpdateOneChoice(index, uc, param);
				repository.Save(uc);
			}
			return true;
		}
		private bool UpdateChoices(List<UserChoice> choices, NameValueCollection param) {
			foreach (var uc in choices) {
				var code = uc.SchemeCode;
				var info = string.Format("{0}_info[{1}]", uc.SchemeCode, uc.Classification.Id);
				if (!uc.Classification.IsMultiChoice) {
					Guid selected = Guid.Empty;
					if (Guid.TryParse(param[code], out selected)) {
						uc.IsSelected = selected.Equals(uc.Classification.Id);
					} else
						uc.IsSelected = false;
					uc.Description = "";
					if (uc.IsSelected && uc.Classification.RequireMoreInfo) {
						if (!string.IsNullOrEmpty(param[info]))
							uc.Description = param[code + "_info"];
						else if (!string.IsNullOrEmpty(param[code + "_info"]))
							uc.Description = param[code + "_info"];
					}
				} else {
					code = string.Format("{0}_checked[{1}]", uc.SchemeCode, uc.Classification.Id);
					bool selected = false;
					if (!string.IsNullOrEmpty(param[code])) {
						string[] sel = param[code].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
						if (sel.Length > 0 )
						bool.TryParse(sel[0], out selected);
					}
					uc.IsSelected = selected;
					uc.Description = "";
					if (selected && uc.Classification.RequireMoreInfo) {
						uc.Description = param[info] ?? "";
					}
				}
				repository.Save(uc);
			}
			return true;
		}
		public bool UpdateHostChoices(string email, Guid id, string group, NameValueCollection param) {
			var register = GetHostRegistration(email);
			if (register.Id.Equals(id)) {
				var choices = GetHostChoices(email, group);
				UpdateChoices(choices, param);
				unitOfWork.Commit();
				return true;
			}
			return false;
		}
		public bool UpdateHostRooms(string email, Guid id, NameValueCollection param) {
			var register = GetHostRegistration(email);
			if (register.Id.Equals(id)) {
				var cbedRooms = repository.AsQueryable<Room>(x => x.HostRegistration.User.Email == email && x.RoomType == 1).Count();
				var cbathRooms = repository.AsQueryable<Room>(x => x.HostRegistration.User.Email == email && x.RoomType == 2).Count();
				if (cbedRooms == 0) 
					AddRooms(register, 1);
				if (cbathRooms == 0) 
					AddRooms(register, 2);
				var bedRooms = repository.AsQueryable<Room>(x => x.HostRegistration.User.Email == email && x.RoomType == 1).OrderBy(x=>x.RoomIndex).ToList();
				var bathRooms = repository.AsQueryable<Room>(x => x.HostRegistration.User.Email == email && x.RoomType == 2).OrderBy(x => x.RoomIndex).ToList();
				for( int i=0; i < bedRooms.Count(); i++) {
					var r = bedRooms[i];
					r.RoomNo = param[string.Format("RoomNo[{0}]", i)];
					DateTime v;
					if (DateTime.TryParse(param[string.Format("RoomAvailableFrom[{0}]", i)], out v))
						r.AvailableFrom = v;
					else r.AvailableFrom = null;
					if (DateTime.TryParse(param[string.Format("RoomAvailableTo[{0}]", i)], out v))
						r.AvailableTo = v;
					else r.AvailableTo = null;
					if (DateTime.TryParse(param[string.Format("RoomDateLeaving[{0}]", i)], out v))
						r.DateLeaving = v;
					else r.DateLeaving = null;
					var infoes = repository.AsQueryable<RoomInfo>(x => x.Room.Id == r.Id).ToList();
					UpdateRoomInfo(i, infoes, param);
				}
				for (int i = 0; i < bathRooms.Count(); i++) {
					var r = bathRooms[i];
					r.RoomNo = param[string.Format("BathRoomNo[{0}]", i)];
					var infoes = repository.AsQueryable<RoomInfo>(x => x.Room.Id == r.Id).ToList();
					UpdateRoomInfo(i, infoes, param);
				}
				unitOfWork.Commit();
				return true;
			}
			return false;
		}
		public bool UpdateHostMembers(string email, Guid id, NameValueCollection param) {
			var register = GetHostRegistration(email);
			if (register.Id.Equals(id)) {
				var cMembers = repository.AsQueryable<Member>(x => x.HostRegistration.User.Email == email).Count();
				if (cMembers < 1) AddMembers(register);
				var members = repository.AsQueryable<Member>(x => x.HostRegistration.User.Email == email).OrderBy(x => x.MemberIndex).ToList();

				for (int i = 0; i < members.Count(); i++) {
					var r = members[i];
					r.FirstName = param[string.Format("FirstName[{0}]", i)];
					r.LastName = param[string.Format("LastName[{0}]", i)];
					r.Occupation = param[string.Format("Occupation[{0}]", i)];
					bool sB = false;
					if (bool.TryParse(param[string.Format("HaveWWCC[{0}]", i)], out sB))
						r.HaveWWCC = sB;
					r.ClearanceNumber = param[string.Format("ClearanceNumber[{0}]", i)];
					DateTime v;
					if (DateTime.TryParse(param[string.Format("BirthDate[{0}]", i)], out v))
						r.BirthDate = v;
					else r.BirthDate = null;
					if (DateTime.TryParse(param[string.Format("ClearanceExpiryDate[{0}]", i)], out v))
						r.ClearanceExpiryDate = v;
					else r.ClearanceExpiryDate = null;
					var infoes = repository.AsQueryable<MemberInfo>(x => x.Member.Id == r.Id).ToList();
					UpdateMemberInfo(i, infoes, param);
					var langs = repository.AsQueryable<MemberLanguage>(x => x.Member.Id == r.Id).OrderBy(x => x.LanguageIndex).ToList();
					for (int j=0; j < 5; j++) {
						string lang = string.Format("L_Languages[{0}][{1}]", i, j);
						string profi = string.Format("L_LanguageProficiency[{0}][{1}]", i, j);
						if (string.IsNullOrEmpty(param[lang])) langs[j].Language = null;
						else langs[j].Language = repository.AsProxy<Classification>(Guid.Parse(param[lang]));
						if (string.IsNullOrEmpty(param[profi])) langs[j].Proficiency = null;
						else langs[j].Proficiency = repository.AsProxy<Classification>(Guid.Parse(param[profi]));
						repository.Save(langs[j]);
					}
				}
				unitOfWork.Commit();
				return true;
			}
			return false;
		}
		public bool UpdateStudentChoices(string email, Guid id, string group, NameValueCollection param) {
			var register = GetStudentRegistration(email);
			if (register.Id.Equals(id)) {
				var choices = GetHostChoices(email, group);
				UpdateChoices(choices, param);
				var langs = repository.AsQueryable<StudentLanguage>(x => x.Student.Id == register.Id).OrderBy(x => x.LanguageIndex).ToList();
				for (int j = 0; j < 5; j++) {
					string lang = string.Format("L_Languages[{0}]", j);
					string profi = string.Format("L_LanguageProficiency[{0}]", j);
					if (string.IsNullOrEmpty(param[lang])) langs[j].Language = null;
					else langs[j].Language = repository.AsProxy<Classification>(Guid.Parse(param[lang]));
					if (string.IsNullOrEmpty(param[profi])) langs[j].Proficiency = null;
					else langs[j].Proficiency = repository.AsProxy<Classification>(Guid.Parse(param[profi]));
					repository.Save(langs[j]);
				}
				unitOfWork.Commit();
				return true;
			}
			return false;
		}
		public StudentRegistration StudentRegister(ViewModels.StudentRegistrationFirstStep sr) {
			StudentRegistration student = new StudentRegistration() {
				Version = 0,
				CreatedByUser = sr.Email,
				ModifiedByUser = sr.Email,
				BirthDate = sr.DateOfBirth,
				HomePhone = sr.HomePhone,
				MobilePhone = sr.MobilePhone,
				PassportNumber = sr.PassportNumber,
				PassportExpiryDate = sr.PassportExpiryDate
			};
			if (string.IsNullOrEmpty(student.HomePhone)) student.HomePhone = "";
			if (string.IsNullOrEmpty(student.MobilePhone)) student.MobilePhone = "";
			student.User = repository.AsProxy<BetterCms.Module.Users.Models.User>(sr.Id);
			student.Gender = repository.AsProxy<Classification>(new Guid(sr.GenderId));
			repository.Save(student);
			for (int j = 0; j < 5; j++) {
				StudentLanguage l = new StudentLanguage() {
					LanguageIndex = j,
					Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
					Student = student
				};
				repository.Save(l);
			}
			unitOfWork.Commit();
			AddStudentChoiceClassifications(student.User);
			return student;
		}
		public HostRegistration HostRegister(HostRegistrationFirstStep hr) {
			HostRegistration host = new HostRegistration() {
				Version = 0,
				CreatedByUser = hr.Email,
				ModifiedByUser = hr.Email,
				BirthDate = hr.DateOfBirth,
				HomePhone = hr.HomePhone,
				MobilePhone = hr.MobilePhone,
				HomeAddress = hr.StreetAddress,
				HomeUnitNbr = hr.Address_UnitNo,
				HomeStreetNbr = hr.Address_StreetNo,
				HomeStreetName = hr.Address_Route,
				HomeCity = hr.Address_City,
				HomePostCode = hr.Address_PostCode,
				HomeState = hr.Address_State,
				PostalSameAsHome = (hr.StreetAddress==hr.PostalAddress),
				PostalAddress = hr.PostalAddress,
				PostalUnitNbr = hr.Postal_UnitNo,
				PostalStreetNbr = hr.Postal_StreetNo,
				PostalStreetName = hr.Postal_Route,
				PostalCity = hr.Postal_City,
				PostalPostCode = hr.Postal_PostCode,
				PostalState = hr.Postal_State
			};
			if (string.IsNullOrEmpty(host.HomePhone)) host.HomePhone = "";
			if (string.IsNullOrEmpty(host.MobilePhone)) host.MobilePhone = "";
			host.User = repository.AsProxy<BetterCms.Module.Users.Models.User>(hr.Id);
			host.Gender = repository.AsProxy<Classification>(new Guid(hr.GenderId));
			repository.Save(host);
			unitOfWork.Commit();
			AddHostChoiceClassifications(host.User);
			AddRooms(host, 1);
			AddRooms(host, 2);
			AddMembers(host);
			return host;
		}

		public void AddStudentChoiceClassifications(BetterCms.Module.Users.Models.User user) {
			List<Classification> choices = classification.GetClassificationsForStudent(string.Empty);
			foreach (var code in choices.Select(x => x.SchemeCode).Distinct()) {
				var cls = choices.Where(x => x.SchemeCode.Equals(code)).OrderBy(x => x.SortOrder);
				foreach (var c in cls) {
					var uc = new UserChoice() {
						SchemeCode = c.SchemeCode, IsSelected = false, Description = "",
						Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
						User = user
					};
					uc.Classification = repository.AsProxy<Classification>(c.Id);
					repository.Save(uc);
				}
			}
			unitOfWork.Commit();
		}
		public void AddHostChoiceClassifications(BetterCms.Module.Users.Models.User user) {
			List<Classification> choices = classification.GetClassificationsForHost(string.Empty);
			foreach (var code in choices.Select(x => x.SchemeCode).Distinct()) {
				var cls = choices.Where(x => x.SchemeCode.Equals(code)).OrderBy(x=>x.SortOrder);
				foreach (var c in cls) {
					var uc = new UserChoice() {
						SchemeCode = c.SchemeCode, IsSelected = false, Description = "",
						Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
						User = user
					};
					uc.Classification = repository.AsProxy<Classification>(c.Id);
					repository.Save(uc);
				}
			}
			unitOfWork.Commit();
		}
		public void AddRooms(HostRegistration register, int type) {
			List<Classification> choices = classification.GetClassificationsForRoom(type.ToString());
			for (int i = 0; i < 5; i++) {
				Room r = new Room() {
					HostRegistration = register,
					RoomType = type,
					RoomIndex = i,
					RoomNo = (i + 1).ToString(),
					Version = 0,
					CreatedByUser = "Anonymous",
					ModifiedByUser = "Anonymous"
				};
				repository.Save(r);
				foreach (var c in choices) {
					var uc = new RoomInfo() {
						SchemeCode = c.SchemeCode, IsSelected = false, Description = "",
						Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
						Room = r
					};
					uc.Classification = repository.AsProxy<Classification>(c.Id);
					repository.Save(uc);
				}
			}
			unitOfWork.Commit();
		}
		public void AddMembers(HostRegistration register) {
			List<Classification> choices = classification.GetClassificationsForMember();
			for (int i = 0; i < 9; i++) {
				Member r = new Member() {
					HostRegistration = register,
					MemberIndex = i, FirstName = "", LastName = "", Occupation = "", HaveWWCC = false, ClearanceNumber = "",
					Version = 0,
					CreatedByUser = "Anonymous",
					ModifiedByUser = "Anonymous"
				};
				repository.Save(r);
				foreach (var c in choices) {
					var uc = new MemberInfo() {
						SchemeCode = c.SchemeCode, IsSelected = false, Description = "",
						Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
						Member = r
					};
					uc.Classification = repository.AsProxy<Classification>(c.Id);
					repository.Save(uc);
				}
				for (int j = 0; j < 5; j++) {
					MemberLanguage l = new MemberLanguage() {
						LanguageIndex = j, Language = null, Proficiency = null,
						Version = 0, CreatedByUser = "Anonymous", ModifiedByUser = "Anonymous",
						Member = r
					};
					repository.Save(l);
				}
			}
			unitOfWork.Commit();
		}
	}
}