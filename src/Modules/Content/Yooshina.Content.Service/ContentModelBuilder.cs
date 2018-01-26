using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yooshina.CMSCore.Model;
using Yooshina.Content.Model;

namespace Yooshina.Content {
	public static class CoreCustomModelBuilder {
		public static void Build(this ModelBuilder modelBuilder) {
			modelBuilder.Entity<ContentCategory>()
				.HasMany(x => x.Childs)
				.WithOne(x => x.Parent)
				.HasForeignKey(x => x.ParentId);
				//.HasOptional(p => p.Parent)
				//.WithMany(p => p.Children)
				//.IsIndependent()
				//.Map(m => m.MapKey(p => p.Id, "ParentID"));

		}
	}
}
