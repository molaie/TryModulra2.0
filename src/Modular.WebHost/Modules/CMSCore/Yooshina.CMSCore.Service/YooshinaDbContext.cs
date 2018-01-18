using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yooshina.CMSCore.Models;
using Yooshina.Core;
using Yooshina.Domain.Models;
using Yooshina.Repository;

namespace Yooshina.CMSCore {

	public class YooshinaDbContext : IdentityDbContext<User, Role, long> {

		public YooshinaDbContext(DbContextOptions options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			List<Type> typeToRegisters = new List<Type>();
			typeToRegisters.AddRange(Assembly.GetAssembly(typeof(YooshinaDbContext)).DefinedTypes.Select(t => t.AsType()));


			modelBuilder.RegisterEntities(typeToRegisters).RegiserConvention();

			//RegisterEntities(modelBuilder, typeToRegisters);

			//RegiserConvention(modelBuilder);

			//base.OnModelCreating(modelBuilder);

			//RegisterCustomMappings(modelBuilder, typeToRegisters);
		}



		private static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters) {

			var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
			foreach (var builderType in customModelBuilderTypes) {
				if (builderType != null && builderType != typeof(ICustomModelBuilder)) {
					var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
					builder.Build(modelBuilder);
				}
			}

		}
	}
}
