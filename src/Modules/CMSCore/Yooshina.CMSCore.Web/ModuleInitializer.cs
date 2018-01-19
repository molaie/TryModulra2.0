using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Yooshina.CMSCore.Model;
using Yooshina.CMSCore.Services;
using Yooshina.Core;
using Yooshina.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Yooshina.CMSCore.Web {

	public class ModuleInitializer : IModuleInitializer {

		public void Init(IServiceCollection services, Container container, Assembly asm, IConfiguration config) {


			services.AddDbContext<YooshinaDbContext>(options =>
				options.UseSqlServer(config.GetConnectionString("YooshinaCoreConnection"))
			);


			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<YooshinaDbContext>()
				.AddDefaultTokenProviders();

			container.Configure(_ => {
				_.For(typeof(IRepository<>)).Use(typeof(Repository<>));
				_.For<IEmailSender>().Use<AuthMessageSender>();
				_.For<ISmsSender>().Use<AuthMessageSender>();
				_.Scan(x => {
					x.Assembly(asm);
					x.AssemblyContainingType<IEmailSender>();
					x.WithDefaultConventions();
				});
			});

		}
	}
}
