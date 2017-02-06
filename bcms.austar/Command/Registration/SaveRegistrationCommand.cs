
using System;
using bcms.austar.Models;
using bcms.austar.Services;
using bcms.austar.ViewModels;
using BetterCms.Module.Root.Mvc;

using BetterModules.Core.Web.Mvc.Commands;


namespace bcms.austar.Command.Registration {
	public class SaveRegistrationCommand : CommandBase, ICommand<HostRegistrationFirstStep, HostRegistration> {
		public IRegistrationService RegistrationService { get; set; }
		public HostRegistration Execute(HostRegistrationFirstStep request) {
			var reg = RegistrationService.HostRegister(request);
			return reg;
		}
	}
}