using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public interface ICoreRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long> {
	}
}
