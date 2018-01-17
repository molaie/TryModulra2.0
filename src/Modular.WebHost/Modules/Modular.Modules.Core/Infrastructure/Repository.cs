using Modular.Modules.Core.Infrastructure;
using Yooshina.Domain;
using Yooshina.Domain.Models;

namespace Modular.Host.Modules.Modular.Modules.Core.Infrastructure {
	public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
        where T : class, IEntityWithTypedId<long>
    {
        public Repository(ModularDbContext context) : base(context)
        {
        }
    }
}
