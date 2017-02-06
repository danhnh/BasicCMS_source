using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bcms.austar.Models;
using BetterModules.Core.DataAccess;
using BetterModules.Core.DataAccess.DataContext;
using Common.Logging;
using BetterCms.Module.Root.Mvc;
using bcms.austar.Exceptions;

namespace bcms.austar.Services {
	public class DefaultUniversityService : IUniversityService {
		/// <summary>
		/// The repository
		/// </summary>
		private IRepository repository;

		/// <summary>
		/// The unit of work
		/// </summary>
		private IUnitOfWork unitOfWork;

		/// <summary>
		/// The logger
		/// </summary>
		private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
		public DefaultUniversityService(IRepository repository, IUnitOfWork unitOfWork) {
			this.repository = repository;
			this.unitOfWork = unitOfWork;
		}
		private bool ValidateUniversity(Guid id, string name, out University university) {
			var query = repository.AsQueryable<University>(s => s.Name == name);
			if (!id.HasDefaultValue()) {
				query = query.Where(s => s.Id != id);
			}

			university = query.FirstOrDefault();
			return university == null;
		}
		public University SaveUniversity(string name, string shortname) {
			return SaveUniversity(name, shortname, Guid.Empty, 0);
		}

		public University SaveUniversity(string name, string shortname, Guid id, int version) {
			var isNew = id.HasDefaultValue();
			University university;

			// Validate
			if (!ValidateUniversity(id, name, out university)) {
				var logMessage = string.Format("University with entered Name {0} already is exists.", name);

				//if (!ignoreUniqueItemException) {
				//	var message = string.Format("University with name {0} is already exists.", name);
				//	throw new UniqueUniversityException(() => message, logMessage);
				//}

				Logger.Info(logMessage);

				return university;
			}

			if (isNew) {
				university = new University();
			} else {
				university = repository.AsQueryable<University>(w => w.Id == id).FirstOne();
			}

			university.Name = name;
			university.ShortName = shortname;
			university.Version = version;

			repository.Save(university);
			unitOfWork.Commit();

			if (isNew) {
				BetterCms.Events.UniversityEvents.Instance.OnUniversityCreated(university);
			} else {
				BetterCms.Events.UniversityEvents.Instance.OnUniversityUpdated(university);
			}

			return university;
		}
	}
}