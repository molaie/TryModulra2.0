using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Modular.Host.Extensions;
using StructureMap;
using Yooshina.CMSCore;
using Yooshina.Core;
using Yooshina.Domain;

namespace Modular.Host {
	public class Startup {

		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IList<ModuleInfo> modules = new List<ModuleInfo>();

		public Startup(IHostingEnvironment env, IConfiguration configuration) {
			_hostingEnvironment = env;
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services) {

			LoadInstalledModules();

			services.Configure<RazorViewEngineOptions>(options => {
				options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
			});
			

			var mvcBuilder = services.AddMvc();

			//Structure Map
			var container = new Container();

			var moduleInitializerInterface = typeof(IModuleInitializer);
			foreach (var module in modules) {
				// Register controller from modules
				mvcBuilder.AddApplicationPart(module.Assembly);

				// DI in modules
				//each module is responsible for extra DI she needs
				var moduleInitializerType = module.Assembly.GetTypes().Where(x => typeof(IModuleInitializer).IsAssignableFrom(x)).FirstOrDefault();
				if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer)) {
					var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
					moduleInitializer.Init(services, container, module.Assembly, Configuration);
				}
			}

			mvcBuilder.AddViewLocalization();

			//main assembly DI
			container.Configure(_ => {
				_.For(typeof(IRepository<>)).Use(typeof(Repository<>));
				_.Scan(x => {
					x.TheCallingAssembly();
					x.AssembliesFromApplicationBaseDirectory();

					x.WithDefaultConventions();
				});
				_.Populate(services);
			});

			return container.GetInstance<IServiceProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseBrowserLink();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			// Serving static file for modules
			foreach (var module in modules) {
				var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
				if (!wwwrootDir.Exists) {
					continue;
				}

				app.UseStaticFiles(new StaticFileOptions() {
					FileProvider = new PhysicalFileProvider(wwwrootDir.FullName),
					RequestPath = new PathString("/" + module.ShortName)
				});
			}

			app.UseAuthentication();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		private void LoadInstalledModules() {
			var moduleRootFolder = new DirectoryInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "Modules"));
			var moduleFolders = moduleRootFolder.GetDirectories();

			foreach (var moduleFolder in moduleFolders) {
				var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
				if (!binFolder.Exists || binFolder.Name.ToLower().Contains(".web")) {
					continue;
				}

				foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories)) {
					Assembly assembly = null;
					try {
						assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
					} catch (FileLoadException ex) {
						if (ex.Message == "Assembly with same name is already loaded") {
							// Get loaded assembly
							assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
						} else {
							throw;
						}
					}

					if (assembly.FullName.Contains(moduleFolder.Name) && assembly.FullName.ToLower().Contains(".web")) {
						modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.FullName });
					}
				}
			}

			GlobalConfiguration.Modules = modules;
		}
	}
}
