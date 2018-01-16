namespace Yooshina.Domain.Models {
	public interface IEntityWithTypedId<TId> {
		TId Id { get; }
	}
}
