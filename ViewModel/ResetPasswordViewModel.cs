using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } // البريد الإلكتروني

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; } // كلمة المرور الجديدة

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } // تأكيد كلمة المرور

		public string Code { get; set; } // رمز إعادة تعيين كلمة المرور
	}
}
