using BetterCms;
using BetterCms.Core.Modules;
using System;
using Autofac;
using BetterCms.Core.Modules.Projections;
using System.Collections.Generic;
using BetterCms.Module.Root;
using BetterModules.Core.Web.Mvc.Extensions;
using BetterModules.Core.Modules.Registration;
using bcms.austar.Services;
using bcms.austar.Registration;

namespace bcms.austar {
	public class AustarModuleDescriptor : CmsModuleDescriptor {
		internal const string ModuleAreaName = "bcms-austar";
		internal const string ModuleName = "austar";
		internal const string ModuleSchemaName = "bcms_austar";
		private readonly AustarJSIncludeDescriptor austarJsModuleIncludeDescriptor;
		public AustarModuleDescriptor(ICmsConfiguration configuration) : base(configuration) {
			austarJsModuleIncludeDescriptor = new AustarJSIncludeDescriptor(this);
		}
		public override string Name {
			get { return ModuleName; }
		}
		public override string Description {
			get { return "Module for Austar website"; }
		}
		public override int Order {
			get {
				return int.MaxValue - 100;
			}
		}
		public override string AreaName {
			get {
				return ModuleAreaName;
			}
		}
		public override string SchemaName {
			get {
				return ModuleSchemaName;
			}
		}
		public override void RegisterModuleTypes(ModuleRegistrationContext context, ContainerBuilder containerBuilder) {
			containerBuilder.RegisterType<DefaultUniversityService>().AsImplementedInterfaces().InstancePerLifetimeScope();
			containerBuilder.RegisterType<DefaultClassificationService>().AsImplementedInterfaces().InstancePerLifetimeScope();
			containerBuilder.RegisterType<DefaultRegistrationService>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
		public override IEnumerable<IPageActionProjection> RegisterSiteSettingsProjections(ContainerBuilder containerBuilder) {
			//return base.RegisterSiteSettingsProjections(containerBuilder);
			return new IPageActionProjection[]
			{
				new SeparatorProjection(9999),
				new LinkActionProjection(new AustarJSIncludeDescriptor(this), page => "loadSiteSettingsUniversities")
					{
						Order = 9999,
						Title = page => "Universities",
						CssClass = page => "bcms-settings-link",
						AccessRole = RootModuleConstants.UserRoles.MultipleRoles(RootModuleConstants.UserRoles.Administration)
					}
			};
		}
		public override IEnumerable<JsIncludeDescriptor> RegisterJsIncludes() {
			return new [] {
				austarJsModuleIncludeDescriptor
				//new JsIncludeDescriptor(this, "bcms.austar.hostregistration")
			};
		}
		private string minJsPath;
		private string minCssPath;

		public override string BaseModulePath {
			get { return VirtualPath.Combine("/", "file", AreaName); }
		}
		public override string MinifiedJsPath {
			get { return minJsPath ?? (minJsPath = VirtualPath.Combine(JsBasePath, string.Format("bcms.{0}.min.js", Name.ToLowerInvariant()))); }
		}
		public override string MinifiedCssPath {
			get { return minCssPath ?? (minCssPath = VirtualPath.Combine(CssBasePath, string.Format("bcms.{0}.min.css", Name.ToLowerInvariant()))); }
		}
	}
}