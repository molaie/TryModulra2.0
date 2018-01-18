using System.ComponentModel.DataAnnotations;

namespace Yooshina.CMSCore.Model.AccountViewModels {
	public class ExternalLoginConfirmationViewModel {
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
