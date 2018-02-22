using AspNetCore.MvcPager;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
		public ViewResult Index() {
			ViewData["Title"] = "Content Category Tree View";
			//var result = _Repo.Query();
			//result = result.Where(x => x.ParentId == null);

			//var finalResult = result
			//	.OrderByDescending(x => x.Id)
			//	.Select(x => new ContentCategoryViewModel() {
			//		Id = x.Id,
			//		Title = x.Title,
			//		ParentId = x.ParentId,
			//		Slug = x.Slug
			//	});

			var tempResult = GetAllMenuItems();


			return View(GetMenu(tempResult, null));
		}



		private IList<ContentCategory> GetAllMenuItems() {
			return _Repo.Query().ToList();
		}

		private IList<ContentCategory> GetChildrenMenu(IList<ContentCategory> menuList, long? parentId = null) {
			return menuList.Where(x => x.ParentId == parentId).OrderBy(x => x.Ordering).ToList();
		}
		private ContentCategory GetMenuItem(IList<ContentCategory> menuList, long id) {
			return menuList.FirstOrDefault(x => x.Id == id);
		}




		private IList<ContentCategoryViewModel> GetMenu(IList<ContentCategory> menuList, long? parentId) {
			var children = GetChildrenMenu(menuList, parentId);

			if (!children.Any()) {
				return new List<ContentCategoryViewModel>();
			}

			var vmList = new List<ContentCategoryViewModel>();
			foreach (var item in children) {
				var menu = GetMenuItem(menuList, item.Id);
				var vm = new ContentCategoryViewModel();
				vm.Id = menu.Id;
				vm.Title = menu.Title;
				vm.Slug = menu.Slug;
				vm.Children = GetMenu(menuList, menu.Id);
				vmList.Add(vm);
			}

			return vmList;
		}







		[ViewLayout("_Dashboard")]
		public ViewResult Index2(int? page, string title) {
			ViewData["Title"] = "Content Category";
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


		public ViewResult Create() {
			ViewData["Title"] = "Create a new portal";
			return View(_Repo.Create());
		}



		[HttpPost]
		public ViewResult Create(ContentCategory item) {
			_Repo.Add(item);
			_Repo.SaveChange();
			ViewData["Title"] = "Create a new portal";
			return View(_Repo.Create());
		}



		public ViewResult Edit(long itemId) {
			ViewData["Title"] = "Edit a portal";
			return View(_Repo.Query().FirstOrDefault(x => x.Id == itemId));
		}








		[HttpGet]
		public JsonResult GetChilds(int? parentID) {

			var result = _Repo.Query();
			result = result.Where(x => x.ParentId == parentID || ((int?)null == x.ParentId && null == parentID));

			return Json(result.AsEnumerable());
		}


	}
}
