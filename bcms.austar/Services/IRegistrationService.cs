using bcms.austar.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace bcms.austar.Services {
	public interface IRegistrationService {
		HostRegistration HostRegister(ViewModels.HostRegistrationFirstStep hr);
		StudentRegistration StudentRegister(ViewModels.StudentRegistrationFirstStep sr);
		bool UpdateHostChoices(string email, Guid id, string group, NameValueCollection param);
		bool UpdateHostRooms(string email, Guid id, NameValueCollection param);
		bool UpdateHostMembers(string email, Guid id, NameValueCollection param);
		bool UpdateStudentChoices(string email, Guid id, string group, NameValueCollection param);
		void AddRooms(HostRegistration register, int type);
		void AddMembers(HostRegistration register);
		HostRegistration GetHostRegistration(string email);
		StudentRegistration GetStudentRegistration(string email);
		HostRegistration UpdateHostRegistration(HostRegistration reg);
		StudentRegistration UpdateStudentRegistration(StudentRegistration reg);
		List<UserChoice> GetHostChoices(string email, string group);
		List<RoomInfo> GetHostRooms(string email);
		List<MemberInfo> GetHostMembers(string email);
		List<MemberLanguage> GetHostMemberLanguages(string email);
		List<StudentLanguage> GetStudentLanguages(string email);
		void AddHostChoiceClassifications(BetterCms.Module.Users.Models.User user);
		void AddStudentChoiceClassifications(BetterCms.Module.Users.Models.User user);
	}
}