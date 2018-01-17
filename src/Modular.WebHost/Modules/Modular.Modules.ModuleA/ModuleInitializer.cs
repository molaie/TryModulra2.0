using ExtraDepenencyTest;
using Microsoft.Extensions.DependencyInjection;
using Modular.Modules.ModuleA.Services;
using Yooshina.Core;

namespace Modular.Modules.ModuleA {
	public class ModuleInitializer : IModuleInitializer
    {
        public void Init(IServiceCollection services)
        {
            services.AddTransient<IAnotherTestService, AnotherTestService>();
            services.AddTransient<ITestService, TestService>();

        }
    }
}
