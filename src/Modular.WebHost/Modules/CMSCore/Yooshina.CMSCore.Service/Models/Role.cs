﻿using Microsoft.AspNetCore.Identity;
using Yooshina.Domain.Models;

namespace Yooshina.CMSCore.Models {

	public class Role : IdentityRole<long>, IEntityWithTypedId<long> {
		public Role() {
		}

		public Role(string name) {
			Name = name;
		}
	}
}
