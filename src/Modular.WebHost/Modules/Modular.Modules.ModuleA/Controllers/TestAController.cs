using ExtraDepenencyTest;
using Microsoft.AspNetCore.Mvc;
using Modular.Modules.ModuleA.Models;
using Modular.Modules.ModuleA.Services;
using Yooshina.Domain;

namespace Modular.Modules.ModuleA.Controllers {
	public class TestAController : Controller
    {
        private ITestService _testService;
        private IAnotherTestService _anotherTestService;
        private IRepository<Sample> _sampleRepository;

        public TestAController(ITestService testService, IAnotherTestService anotherTestService, IRepository<Sample> sampleRepository)
        {
            _testService = testService;
            _anotherTestService = anotherTestService;
            _sampleRepository = sampleRepository;
        }

        public IActionResult Index()
        {
            ViewBag.TestData = _testService.Test();
            ViewBag.AnotherTestData = _anotherTestService.Test();

            var sample = new Sample { Name = "Name test", Description = "Decription Test" };
            _sampleRepository.Add(sample);
            _sampleRepository.SaveChange();

            return View();
        }


		public ViewResult Test2() {
			ViewBag.TestData = _testService.Test();
			ViewBag.AnotherTestData = _anotherTestService.Test();

			var sample = new Sample { Name = "Name test", Description = "Decription Test" };
			_sampleRepository.Add(sample);
			_sampleRepository.SaveChange();

			return View();
		}



	}
}
