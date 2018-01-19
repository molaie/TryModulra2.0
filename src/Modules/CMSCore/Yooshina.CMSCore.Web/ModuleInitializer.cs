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


			services.Configure<IdentityOptions>(options => {
				// Password settings
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 4;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;
				// User settings
				options.User.RequireUniqueEmail = false;
			});

			services.ConfigureApplicationCookie(options => {
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromHours(1);
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
				options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
				options.SlidingExpiration = true;
			});



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
