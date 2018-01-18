using System;
using System.Collections.Generic;
using System.Text;

namespace ContentModule.Poco {

	public abstract class LogEntity {
		public int? CreatedBy { set; get; }
		public int? ModifiedBy { set; get; }
		public DateTime? CreateDateTime { set; get; }
		public DateTime? LastModifiedDateTime { set; get; }
	}
}
