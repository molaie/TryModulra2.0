using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yooshina.Content.ViewModels;

namespace Yooshina.Content.Web.ViewComponents {


	public class ContentCategoriesViewComponent : ViewComponent {



		//private ContentCategoryViewModel contentCategory;

		public ContentCategoriesViewComponent() {

		}


		public IViewComponentResult Invoke(IEnumerable<ContentCategoryViewModel> ccVm) {
			//contentCategory = ccVm;
			return View(ccVm);
		}



	}
}
