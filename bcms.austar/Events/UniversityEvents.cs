using bcms.austar.Models;
using BetterModules.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterCms.Events {
	public class UniversityEvents : EventsBase<UniversityEvents> {
		/// <summary>
		/// Occurs when a university is created.
		/// </summary>
		public event DefaultEventHandler<SingleItemEventArgs<University>> UniversityCreated;

		/// <summary>
		/// Occurs when a university is updated.
		/// </summary>
		public event DefaultEventHandler<SingleItemEventArgs<University>> UniversityUpdated;

		/// <summary>
		/// Occurs when a university is deleted.
		/// </summary>
		public event DefaultEventHandler<SingleItemEventArgs<University>> UniversityDeleted;

		/// <summary>
		/// Called when university is created.
		/// </summary>
		/// <param name="university">The university.</param>
		public void OnUniversityCreated(University university) {
			if (UniversityCreated != null) {
				UniversityCreated(new SingleItemEventArgs<University>(university));
			}
		}

		/// <summary>
		/// Called when university is updates.
		/// </summary>
		/// <param name="university">The university.</param>
		public void OnUniversityUpdated(University university) {
			if (UniversityUpdated != null) {
				UniversityUpdated(new SingleItemEventArgs<University>(university));
			}
		}

		/// <summary>
		/// Called when university is deleted.
		/// </summary>
		/// <param name="university">The university.</param>
		public void OnUniversityDeleted(University university) {
			if (UniversityDeleted != null) {
				UniversityDeleted(new SingleItemEventArgs<University>(university));
			}
		}
	}
}