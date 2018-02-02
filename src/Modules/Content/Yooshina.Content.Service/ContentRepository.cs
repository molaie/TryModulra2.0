
using Yooshina.Content;
using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public class ContentRepository<T> : RepositoryWithTypedId<T, long>, IContentRepository<T>

		where T : class, IEntityWithTypedId<long>, new() {


		public ContentRepository(ContentDbContext context) {
			Context = context;
			DbSet = Context.Set<T>();
		}

	}
}
