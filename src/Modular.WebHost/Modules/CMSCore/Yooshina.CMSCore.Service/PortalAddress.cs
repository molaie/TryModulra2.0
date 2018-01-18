using System;
using System.Collections.Generic;
using System.Text;

namespace ContentModule.Poco {

	public class PortalAddress {
		public PortalAddress() {
		}

		public int ID { get; set; }
		public int PortalID { get; set; }
		public string DomainAddress { get; set; }

		public virtual Portal Portal { get; set; }

	}
}
