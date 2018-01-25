using Microsoft.AspNetCore.Mvc;

namespace Yooshina.CMSCore.Web.Controllers {
	public class HomeController : Controller {

		public HomeController() {
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult Error() {
			return View();
		}
	}
}
