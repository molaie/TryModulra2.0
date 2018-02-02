using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yooshina.Repository;

namespace Yooshina.Content {

	public class ContentDbContext : DbContext {

		public ContentDbContext(DbContextOptions<ContentDbContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			List<Type> typeToRegisters = new List<Type>();
			typeToRegisters.AddRange(Assembly.GetAssembly(typeof(ContentDbContext)).DefinedTypes.Select(t => t.AsType()));

			modelBuilder.RegisterEntities(typeToRegisters).RegiserConvention();

			base.OnModelCreating(modelBuilder);

			modelBuilder.Build();
		}

	}
}
