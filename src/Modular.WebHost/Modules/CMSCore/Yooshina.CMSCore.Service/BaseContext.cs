using ContentModule.Poco;
using Microsoft.EntityFrameworkCore;

namespace ContentModule {

	public class BaseContext : DbContext {

		public BaseContext(DbContextOptions<BaseContext> options) : base(options) {

		}

		public DbSet<Portal> Portal { get; set; }
		public DbSet<PortalAddress> PortalAddresses { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			base.OnConfiguring(optionsBuilder);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Portal>().ToTable("Portal");
			modelBuilder.Entity<PortalAddress>().ToTable("PortalAddress");
		}
	}
}
