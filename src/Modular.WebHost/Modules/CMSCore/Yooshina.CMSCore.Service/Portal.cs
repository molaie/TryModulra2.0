using System;
using System.Collections.Generic;
using System.Text;

namespace ContentModule.Poco {

	public class Portal : LogEntity {

		public Portal() {
			this.Childs = new HashSet<Portal>();
		}

		public int ID { get; set; }
		public string Title { get; set; }
		public Nullable<int> ParentID { get; set; }
		public string Description { get; set; }
		public string Alias { get; set; }
		public string Direction { get; set; }
		public string Language { get; set; }
		public string Domain { get; set; }
		public string Favicon { get; set; }
		public string MetaDescription { get; set; }
		public string FolderAlias { get; set; }
		public int Qouta { get; set; }
		public bool ShowHerOwnLogo { get; set; }
		public virtual Portal Parent { get; set; }

		public virtual ICollection<Portal> Childs { get; set; }

		public virtual ICollection<PortalAddress> PortalAddresses { get; set; }

	}
}
