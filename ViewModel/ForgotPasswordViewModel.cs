using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } 
	}

}
