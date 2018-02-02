using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public class CoreRepository<T> : RepositoryWithTypedId<T, long>, ICoreRepository<T>
		where T : class, IEntityWithTypedId<long>, new() {


		public CoreRepository(YooshinaDbContext context) {
			Context = context;
			DbSet = Context.Set<T>();
		}

	}
}
