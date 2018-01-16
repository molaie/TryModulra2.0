using Microsoft.EntityFrameworkCore;

namespace Yooshina.Core {
	public interface ICustomModelBuilder {
		void Build(ModelBuilder modelBuilder);
	}
}
