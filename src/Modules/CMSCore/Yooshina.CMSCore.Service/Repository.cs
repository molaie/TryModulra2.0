using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
		where T : class, IEntityWithTypedId<long> {


		public Repository(YooshinaDbContext context) : base(context) {
		}

	}
}
