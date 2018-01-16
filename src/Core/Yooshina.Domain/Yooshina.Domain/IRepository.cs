using Yooshina.Domain.Models;

namespace Yooshina.Domain {
	public interface IRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long> {
	}
}
