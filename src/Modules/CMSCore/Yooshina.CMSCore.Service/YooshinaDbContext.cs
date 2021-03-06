﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yooshina.CMSCore.Model;
using Yooshina.Repository;

namespace Yooshina.CMSCore {

	public class YooshinaDbContext : IdentityDbContext<User, Role, long> {

		public YooshinaDbContext(DbContextOptions<YooshinaDbContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			List<Type> typeToRegisters = new List<Type>();
			typeToRegisters.AddRange(Assembly.GetAssembly(typeof(YooshinaDbContext)).DefinedTypes.Select(t => t.AsType()));


			modelBuilder.RegisterEntities(typeToRegisters).RegiserConvention();

			base.OnModelCreating(modelBuilder);

			modelBuilder.Build();
		}

	}
}
