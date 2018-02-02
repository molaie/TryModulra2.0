using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System.Reflection;
using Yooshina.Content;
using Yooshina.Content.Model;
using Yooshina.Core;

namespace Yooshina.CMSCore.Web {

	public class ModuleInitializer : IModuleInitializer {

		public void Init(IServiceCollection services, Container container, Assembly asm, IConfiguration config) {


			services.AddDbContext<ContentDbContext>(options =>
				options.UseSqlServer(config.GetConnectionString("YooshinaCoreConnection"))
			);

			container.Configure(_ => {
				_.For(typeof(IContentRepository<>)).Use(typeof(ContentRepository<>));
				_.Scan(x => {
					x.Assembly(asm);
					x.AssemblyContainingType<ContentCategory>();
					x.WithDefaultConventions();
				});
			});

		}
	}
}
