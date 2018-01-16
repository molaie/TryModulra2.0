namespace Yooshina.Domain.Models {
	public class EntityWithTypedId<TId> : IEntityWithTypedId<TId> {
		public TId Id { get; protected set; }
	}
}
