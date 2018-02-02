using AspNetCore.MvcPager;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Yooshina.Content.Model;
using Yooshina.Content.ViewModels;
using Yooshina.Web.Common;

namespace Yooshina.Content.Web.Controllers {

	public class ContentCategoryController : Controller {

		private IContentRepository<ContentCategory> _Repo;
		int PageSize = 4;


		public ContentCategoryController(IContentRepository<ContentCategory> portalRepo) {
			_Repo = portalRepo;
		}


		[ViewLayout("_Dashboard")]
		public ViewResult Index(int? page, string title) {

			var cPage = page ?? 1;
			if (cPage < 1) {
				cPage = 1;
			}
			var result = _Repo.Query();
			if (!string.IsNullOrWhiteSpace(title)) {
				result = result.Where(x => x.Title.Contains(title));
			}

			var finalResult = result
				.OrderByDescending(x => x.Id)
				.Skip(PageSize * (cPage - 1))
				.Take(PageSize)
				.Select(x => new ContentCategoryViewModel() {
					Id = x.Id,
					Title = x.Title,
					ParentId = x.ParentId
				});

			return View(
				new PagedModel<ContentCategoryViewModel>(finalResult.AsEnumerable(), new PagingInfo())
			);
		}
	}
}
