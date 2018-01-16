using Microsoft.Extensions.DependencyInjection;

namespace Yooshina.Core {
	public interface IModuleInitializer {
		void Init(IServiceCollection serviceCollection);
	}
}
