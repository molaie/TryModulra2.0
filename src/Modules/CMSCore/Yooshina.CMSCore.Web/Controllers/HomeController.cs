using Microsoft.AspNetCore.Mvc;

namespace Yooshina.CMSCore.Web.Controllers {
	public class HomeController : Controller {

		//private readonly IContainer _Container;


		public HomeController(
			//IContainer _c2
			) {
			//_Container = _c2;
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult About() {
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact() {
			ViewData["Message"] = "Your contact page.";

			return View();
		}


		public IActionResult SM() {
			//var txt = _Container.WhatDoIHave(typeName: "Sample").Replace("\\r\\n", "<br />");
			//txt = _Container.WhatDoIHave().Replace("\\r\\n", "<br />");
			//ViewData["Message"] = txt;
			return View();
		}



		public IActionResult Error() {
			return View();
		}
	}
}
