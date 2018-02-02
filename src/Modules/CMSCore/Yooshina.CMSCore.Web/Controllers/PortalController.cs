using AspNetCore.MvcPager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Yooshina.CMSCore.Model;
using Yooshina.CMSCore.ViewModels.PortalViewModels;
using Yooshina.Domain;
using Yooshina.Web.Common;

namespace Yooshina.CMSCore.Web.Controllers {

	[Authorize]
	[ViewLayout("_Dashboard")]
	public class PortalController : Controller {


		private IRepository<Portal> _Repo;
		int PageSize = 4;


		public PortalController(IRepository<Portal> portalRepo) {
			_Repo = portalRepo;
		}



		public ViewResult Index(int? page, string title, string alias) {
			ViewData["Title"] = "Portals";
			var cPage = page ?? 1;
			if (cPage < 1) {
				cPage = 1;
			}
			var result = _Repo.Query();
			if (!string.IsNullOrWhiteSpace(title)) {
				result = result.Where(x => x.Title.Contains(title));
			}
			if (!string.IsNullOrWhiteSpace(alias)) {
				result = result.Where(x => x.Alias.Contains(alias));
			}

			var finalResult = result
				.OrderByDescending(x => x.Id)
				.Skip(PageSize * (cPage - 1))
				.Take(PageSize)
				.Select(x => new PortalViewModel() {
					Id = x.Id,
					Alias = x.Alias,
					Direction = x.Direction,
					Favicon = x.Favicon,
					FolderAlias = x.FolderAlias,
					Language = x.Language,
					Title = x.Title
				});

			return View(
				new PagedModel<PortalViewModel>(finalResult.AsEnumerable(), new PagingInfo())
			);
		}


		public ViewResult Create() {
			ViewData["Title"] = "Create a new portal";
			return View(new Portal());
		}


		public ViewResult Edit(int portalId) {
			ViewData["Title"] = "Edit a portal";
			return View(_Repo.Query().FirstOrDefault(x => x.Id == portalId));
		}




		//[HttpPost]
		//public ViewResult Create(Portal P) {
		//	if (!ModelState.IsValid) {
		//		return View();
		//	}
		//}



	}
}
