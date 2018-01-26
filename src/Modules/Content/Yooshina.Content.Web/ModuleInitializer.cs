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
using Yooshina.Content.Model;

namespace Yooshina.CMSCore.Web {

	public class ModuleInitializer : IModuleInitializer {

		public void Init(IServiceCollection services, Container container, Assembly asm, IConfiguration config) {


			services.AddDbContext<ContentDbContext>(options =>
				options.UseSqlServer(config.GetConnectionString("YooshinaCoreConnection"))
			);

			container.Configure(_ => {
				_.Scan(x => {
					x.Assembly(asm);
					x.AssemblyContainingType<ContentCategory>();
					x.WithDefaultConventions();
				});
			});

		}
	}
}
