using System.ComponentModel.DataAnnotations;

namespace JiuJitsuWebApp.Models
{
    public class UserRegisterRequests
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Please provide a password with at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; } = string.Empty;

        [Required, MinLength(2, ErrorMessage = "Please provide a password with at least 2 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required, MinLength(2, ErrorMessage = "Please provide a password with at least 2 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required, Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
