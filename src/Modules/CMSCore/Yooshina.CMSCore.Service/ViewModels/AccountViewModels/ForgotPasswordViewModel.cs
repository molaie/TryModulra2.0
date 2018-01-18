using System.ComponentModel.DataAnnotations;

namespace Yooshina.CMSCore.Model.AccountViewModels {
	public class ForgotPasswordViewModel {
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
