using Microsoft.EntityFrameworkCore;
using System.Linq;
using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public abstract class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>, new() {


		protected DbContext Context { get; set; }

		protected DbSet<T> DbSet { get; set; }

		public void Add(T entity) {
			DbSet.Add(entity);
		}

		public void SaveChange() {
			Context.SaveChanges();
		}

		public IQueryable<T> Query() {
			return DbSet;
		}

		public void Remove(T entity) {
			DbSet.Remove(entity);
		}

		public T Create() {
			return new T();
		}
	}
}
