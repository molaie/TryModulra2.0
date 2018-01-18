using System;

namespace Yooshina.Domain.Models {

	public abstract class LogEntityWithTypedId: Entity {

		public long? CreatedBy { set; get; }
		public long? ModifiedBy { set; get; }
		public DateTime? CreateDateTime { set; get; }
		public DateTime? LastModifiedDateTime { set; get; }
	}
}
