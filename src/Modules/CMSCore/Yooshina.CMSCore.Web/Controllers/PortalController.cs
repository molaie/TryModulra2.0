using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yooshina.CMSCore.Model;
using Yooshina.CMSCore.ViewModels.PortalViewModels;
using Yooshina.Domain;

namespace Yooshina.CMSCore.Web.Controllers {

	[Authorize]
	public class PortalController : Controller {


		private IRepository<Portal> _portalRepo;


		public PortalController(IRepository<Portal> portalRepo) {
			_portalRepo = portalRepo;
		}

		public ViewResult Index() {
			var allPortals = _portalRepo.Query().Select(
				x=> new PortalViewModel() {
					Alias = x.Alias,
					Direction = x.Direction,
					Favicon = x.Favicon,
					FolderAlias = x.FolderAlias,
					Language = x.Language,
					Title = x.Title
				});
			return View(allPortals);
		}


	}
}
