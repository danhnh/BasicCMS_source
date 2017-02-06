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
	public class DefaultClassificationService : IClassificationService {
		private IRepository repository;
		private IUnitOfWork unitOfWork;
		private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
		public DefaultClassificationService(IRepository repository, IUnitOfWork unitOfWork) {
			this.repository = repository;
			this.unitOfWork = unitOfWork;
		}
		public List<Models.Classification> GetClassificationsForRoom(string type) {
			var query = repository.AsQueryable<Classification>().Where(x => x.SchemeCode.StartsWith("R"+type+"_"));
			return query.ToList();
		}
		public List<Models.Classification> GetClassificationsForMember() {
			var query = repository.AsQueryable<Classification>().Where(x => x.SchemeCode.StartsWith("M_"));
			return query.ToList();
		}
		public List<Models.Classification> GetClassificationsForLanguage() {
			var query = repository.AsQueryable<Classification>().Where(x => x.SchemeCode.StartsWith("L_"));
			return query.ToList();
		}
		public List<Classification> GetClassifications(string request) {
			var query = repository.AsQueryable<Classification>();
			if (!string.IsNullOrWhiteSpace(request)) {
				request = request.ToLower();
				query = query.Where(a => "".Equals(a.SchemeCode) || a.SchemeCode.ToLower().Equals(request));
			}
			return query.ToList();
		}
		public List<Classification> GetClassificationsForHost(string group) {
			var query = repository.AsQueryable<Classification>().Where(x => x.SchemeCode.StartsWith("H_"));
			if (!string.IsNullOrEmpty(group)) query = query.Where(x => x.Group == group);
			return query.ToList();
		}
		public List<Classification> GetClassificationsForStudent(string group) {
			var query = repository.AsQueryable<Classification>().Where(x => x.SchemeCode.StartsWith("S_"));
			if (!string.IsNullOrEmpty(group)) query = query.Where(x => x.Group == group);
			return query.ToList();
		}
		public List<Classification> GetCommonClassifications(string group) {
			var query = repository.AsQueryable<Classification>().Where(x => !x.SchemeCode.StartsWith("H_") && !x.SchemeCode.StartsWith("S_"));
			if (!string.IsNullOrEmpty(group)) query = query.Where(x => x.Group == group);
			return query.ToList();
		}
	}
}