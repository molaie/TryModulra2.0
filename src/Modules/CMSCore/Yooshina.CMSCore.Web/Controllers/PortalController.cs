using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Yooshina.CMSCore.Model;
using Yooshina.Domain;

namespace Yooshina.CMSCore.Web.Controllers {

	[Authorize]
	public class PortalController : Controller {


		private IRepository<Portal> _portalRepo;


		public PortalController(IRepository<Portal> portalRepo) {
			_portalRepo = portalRepo;
		}

		public ViewResult Index() {
			return null;

		}


	}
}
