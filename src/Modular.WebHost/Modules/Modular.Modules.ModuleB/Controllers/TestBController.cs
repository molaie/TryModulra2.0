using Microsoft.AspNetCore.Mvc;
using Modular.Modules.ModuleB.Models;
using Yooshina.Domain;

namespace Modular.Modules.ModuleB.Controllers {
	public class TestBController : Controller
    {
        private IRepository<Sample> _sampleRepository;

        public TestBController(IRepository<Sample> sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public IActionResult Index()
        {
            var sample = new Sample { Name = "Text" };
            _sampleRepository.Add(sample);
            _sampleRepository.SaveChange();
            return View();
        }
    }
}
