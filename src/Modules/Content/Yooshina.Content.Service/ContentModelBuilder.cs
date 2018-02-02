using Microsoft.EntityFrameworkCore;
using Yooshina.Content.Model;

namespace Yooshina.Content {
	public static class CoreCustomModelBuilder {
		public static void Build(this ModelBuilder modelBuilder) {


			//modelBuilder.Entity<ContentCategory>(b => {
			//	b.HasKey(rc => rc.Id);
			//	b.ToTable("Content_Categories");
			//});


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
