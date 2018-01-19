using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Yooshina.Core;
using Yooshina.Domain.Models;

namespace Yooshina.Repository {

	public static class DBContextExtensions {

		public static ModelBuilder RegisterEntities(this ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters) {
			var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(Entity)) && !x.GetTypeInfo().IsAbstract);
			foreach (var type in entityTypes) {
				modelBuilder.Entity(type);
			}
			return modelBuilder;
		}


		public static ModelBuilder RegiserConvention(this ModelBuilder modelBuilder) {
			foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
				if (entity.ClrType.Namespace != null) {
					var nameParts = entity.ClrType.Namespace.Split('.');
					var tableName = string.Concat(nameParts[2], "_", entity.ClrType.Name);
					modelBuilder.Entity(entity.Name).ToTable(tableName);
				}
			}
			return modelBuilder;
		}


		//public static void RegisterCustomMappings(this ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters) {
		//	var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
		//	foreach (var builderType in customModelBuilderTypes) {
		//		if (builderType != null && builderType != typeof(ICustomModelBuilder)) {
		//			var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
		//			builder.Build(modelBuilder);
		//		}
		//	}

		//}

	}
}
