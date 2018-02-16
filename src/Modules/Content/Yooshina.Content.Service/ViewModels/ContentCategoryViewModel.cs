using System.Collections.Generic;

namespace Yooshina.Content.ViewModels {

	public class ContentCategoryViewModel {

		public long Id { get; set; }
		public string Title { get; set; }
		public long? ParentId { get; set; }
		public string Slug { get; set; }

		public IList<ContentCategoryViewModel> Children { get; set; }

	}
}
