using System.Collections.Generic;

namespace Yooshina.Core {

	public static class GlobalConfiguration {

		static GlobalConfiguration() {
			Modules = new List<ModuleInfo>();
		}

		public static IList<ModuleInfo> Modules { get; set; }
	}
}
