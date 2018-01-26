﻿using System;
using System.Collections.Generic;
using System.Text;
using Yooshina.Domain.Models;

namespace Yooshina.Content.Model {

	public class ContentCategory : LogEntityWithTypedId {
		public string Title { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public int? ParentId { get; set; }
		public int Ordering { get; set; }
		public bool Enabled { get; set; }
		public virtual ContentCategory Parent { get; set; }
		public virtual ICollection<ContentCategory> Childs { get; set; }

	}
}
