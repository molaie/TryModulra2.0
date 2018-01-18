using Yooshina.Domain.Models;

namespace ContentModule.Poco {

	public class PortalAddress : Entity {

		public PortalAddress() {
		}

		public long PortalId { get; set; }
		public string DomainAddress { get; set; }

		public virtual Portal Portal { get; set; }

	}
}
