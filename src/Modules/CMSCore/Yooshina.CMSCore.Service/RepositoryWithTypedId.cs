using System.Linq;
using Microsoft.EntityFrameworkCore;
using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore {
	public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>, new() {

		public RepositoryWithTypedId(YooshinaDbContext context) {
			Context = context;
			DbSet = Context.Set<T>();
		}

		protected DbContext Context { get; }

		protected DbSet<T> DbSet { get; }

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
