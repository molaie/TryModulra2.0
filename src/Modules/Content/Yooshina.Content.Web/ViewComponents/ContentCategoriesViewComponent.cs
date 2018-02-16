using Microsoft.AspNetCore.Mvc;
using Yooshina.Content.ViewModels;

namespace Yooshina.Content.Web.ViewComponents {


	public class ContentCategoriesViewComponent : ViewComponent {



		//private ContentCategoryViewModel contentCategory;

		public ContentCategoriesViewComponent() {

		}


		public IViewComponentResult Invoke(ContentCategoryViewModel ccVm) {
			//contentCategory = ccVm;
			return View(ccVm);
		}



	}
}
