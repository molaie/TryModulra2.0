using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.Content {
	public interface IContentRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long> {
	}
}
