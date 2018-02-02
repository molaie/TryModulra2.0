using Microsoft.EntityFrameworkCore;

namespace Yooshina.Domain {
	public interface IUnitOfWork {

		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		int SaveChanges(int UserID);

		int SaveChanges();

		void RejectChanges();

		void Reload();

	}
}
