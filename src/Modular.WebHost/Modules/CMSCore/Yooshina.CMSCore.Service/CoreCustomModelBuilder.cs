﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yooshina.CMSCore.Model;

namespace Yooshina.CMSCore {
	public static class CoreCustomModelBuilder {
		public static void Build(this ModelBuilder modelBuilder) {
			modelBuilder.Entity<User>()
				.ToTable("User");

			modelBuilder.Entity<Role>()
				.ToTable("Roles");

			modelBuilder.Entity<IdentityUserClaim<long>>(b => {
				b.HasKey(uc => uc.Id);
				b.ToTable("UserClaims");
			});

			modelBuilder.Entity<IdentityRoleClaim<long>>(b => {
				b.HasKey(rc => rc.Id);
				b.ToTable("RoleClaims");
			});

			modelBuilder.Entity<IdentityUserRole<long>>(b => {
				b.HasKey(r => new { r.UserId, r.RoleId });
				b.ToTable("UserRoles");
			});

			modelBuilder.Entity<IdentityUserLogin<long>>(b => {
				b.ToTable("UserLogins");
			});

			modelBuilder.Entity<IdentityUserToken<long>>(b => {
				b.ToTable("UserTokens");
			});

			//modelBuilder.Entity<Portal>




		}
	}
}
