using Yooshina.Domain.Models;

namespace Yooshina.CMSCore.Model {

	public class YooshinaModule : LogEntityWithTypedId {

		public string Title { get; set; }
		public string Description { get; set; }
		public string Version { get; set; }
	}
}
