using System.ComponentModel.DataAnnotations;

namespace JiuJitsuWebApp.Models
{
	public class ResetPasswordRequest
	{
		[Required]
		public string PasswordResetToken { get; set; } = string.Empty;

		[Required, MinLength(6, ErrorMessage = "Please provide a password with at least 6 characters")]
		public string Password { get; set; } = string.Empty;

		[Required, Compare(nameof(Password))]
		public string? ConfirmPassword { get; set; } = string.Empty;
	}
}
 