using Yooshina.Domain.Models;

namespace Yooshina.CMSCore.Model {

	public class PortalAddress : Entity {

		public PortalAddress() {
		}

		public long PortalId { get; set; }
		public string DomainAddress { get; set; }

		public virtual Portal Portal { get; set; }

	}
}
