using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.MvcPager {
	public class PagedModel<T> {

		public PagedModel(IEnumerable<T> _selectedModel, PagingInfo _PagingInfo) {
			Model = _selectedModel;
			PagingInfo = _PagingInfo;
		}

		public IEnumerable<T> Model { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
