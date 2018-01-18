using System;
using Microsoft.AspNetCore.Identity;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore.Models {
	public class User : IdentityUser<long>, IEntityWithTypedId<long> {
		public User() {
			CreatedOn = DateTime.Now;
			UpdatedOn = DateTime.Now;
		}

		public string FullName { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime UpdatedOn { get; set; }
	}
}
