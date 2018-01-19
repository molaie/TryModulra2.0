using System.Linq;
using Yooshina.Domain.Models;

namespace Yooshina.Domain {

	public interface IRepositoryWithTypedId<T, in TId> where T : IEntityWithTypedId<TId> {
		IQueryable<T> Query();

		void Add(T entity);

		void SaveChange();

		void Remove(T entity);

		T Create();

	}
}
