using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Modular.Core;
using Modular.Core.Domain;
using Modular.Host.Extensions;
using Modular.Host.Modules.Modular.Modules.Core.Infrastructure;
using Modular.Modules.Core.Infrastructure;
using Modular.Modules.Core.Models;

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

			services.AddDbContext<ModularDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Modular.WebHost")));

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<ModularDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<RazorViewEngineOptions>(options => {
				options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
			});

			var mvcBuilder = services.AddMvc();
			var moduleInitializerInterface = typeof(IModuleInitializer);
			foreach (var module in modules) {
				// Register controller from modules
				mvcBuilder.AddApplicationPart(module.Assembly);

				// Register dependency in modules
				var moduleInitializerType = module.Assembly.GetTypes().Where(x => typeof(IModuleInitializer).IsAssignableFrom(x)).FirstOrDefault();
				if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer)) {
					var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
					moduleInitializer.Init(services);
				}
			}

			// TODO: break down to new method in new class
			var builder = new ContainerBuilder();
			builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
			builder.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));
			foreach (var module in GlobalConfiguration.Modules) {
				builder.RegisterAssemblyTypes(module.Assembly).AsImplementedInterfaces();
			}

			builder.RegisterInstance(Configuration);
			builder.RegisterInstance(_hostingEnvironment);
			builder.Populate(services);
			var container = builder.Build();
			return container.Resolve<IServiceProvider>();
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
				if (!binFolder.Exists) {
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

					if (assembly.FullName.Contains(moduleFolder.Name)) {
						modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.FullName });
					}
				}
			}

			GlobalConfiguration.Modules = modules;
		}
	}
}
